
using OmniMarket.Common.Dtos.Product;

namespace OmniMarket.Application.Features.Product.Queries
{
    public class GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<GetProductByIdQuery, ProductDto>
    {

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(request.Id,cancellationToken);
            return product != null ? mapper.Map<ProductDto>(product) : null;
        }
    }
}
