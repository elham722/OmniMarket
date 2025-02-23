
using OmniMarket.Application.Features.Product.Requests.Queries;

namespace OmniMarket.Application.Features.Product.Handlers.Queries
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDto>
    {
        public Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
