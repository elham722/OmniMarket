
namespace OmniMarket.Application.Features.Product.Commands
{
    public class UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<UpdateProductCommand, Guid>
    {
        public async Task<Guid> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(request.Id,cancellationToken);
           //if (product == null)
              //  throw new NotFoundException(nameof(Product), request.Id);
            mapper.Map(request,product);
            if (request.ProductImages.Any())
            {
                product.ProductImages.Clear();
                product.ProductImages = mapper.Map<List<ProductImage>>(request.ProductImages);
            }
            await productRepository.UpdateAsync(product,cancellationToken);
            return product.Id;
        }
    }
}
