
namespace OmniMarket.Tests.Products.Commands
{
    public class CreateProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly IMapper _mapper;

        public CreateProductCommandHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Name = "لپ‌تاپ",
                Description = "لپ‌تاپ با کارایی بالا",
                Price = 1000m,
                Stock = 10,
                ProductImages = new List<CreateProductImageDto>
                {
                    new CreateProductImageDto { Url = "https://example.com/image1.jpg", IsPrimary = true }
                }
            };

            var product = _mapper.Map<Product>(command);
            var expectedProductId = Guid.NewGuid();
            product.Id = expectedProductId;

            _productRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(product);

            var handler = new CreateProductCommandHandler(_productRepositoryMock.Object, _mapper);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("محصول با موفقیت ایجاد شد.", result.Message);
            Assert.Empty(result.Errors);
            Assert.Equal(expectedProductId, result.Id);
            _productRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task Handle_InvalidRequest_ReturnsFailureResponseWithValidationErrors()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Name = "", // نام خالی - خطای اعتبارسنجی
                Description = "لپ‌تاپ با کارایی بالا",
                Price = -1000m, // قیمت منفی - خطای اعتبارسنجی
                Stock = -10, // موجودی منفی - خطای اعتبارسنجی
                ProductImages = new List<CreateProductImageDto>()
            };

            var handler = new CreateProductCommandHandler(_productRepositoryMock.Object, _mapper);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("ایجاد محصول ناموفق بود.", result.Message);
            Assert.NotEmpty(result.Errors);
            Assert.Contains("نام محصول اجباری است.", result.Errors);
            Assert.Contains("قیمت باید بزرگ‌تر یا مساوی صفر باشد.", result.Errors);
            Assert.Contains("موجودی باید بزرگ‌تر یا مساوی صفر باشد.", result.Errors);
            Assert.Equal(Guid.Empty, result.Id);
            _productRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Never());
        }
    }
}