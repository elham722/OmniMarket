
namespace OmniMarket.Application.Features.Product.Commands
{
    public class CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<CreateProductCommand, Guid>
    {
        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = mapper.Map<Domain.Entities.Product>(request);
            product = await productRepository.AddAsync(product,cancellationToken);
            return product.Id;
        }
    }
}
