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
        public async Task Handle_ValidRequest_ReturnsProductId()
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
            product.Id = Guid.NewGuid();

            _productRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(product);

            var handler = new CreateProductCommandHandler(_productRepositoryMock.Object, _mapper);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotEqual(Guid.Empty, result);
            Assert.Equal(product.Id, result);
            _productRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}