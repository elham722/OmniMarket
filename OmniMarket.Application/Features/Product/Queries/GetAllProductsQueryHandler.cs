
namespace OmniMarket.Application.Features.Product.Queries
{
    public class GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        : IRequestHandler<GetAllProductsQuery, IReadOnlyList<ProductDto>>
    {
        public async Task<IReadOnlyList<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await productRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IReadOnlyList<ProductDto>>(products);
        }
    }
}
