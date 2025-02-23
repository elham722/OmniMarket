
namespace OmniMarket.Application.Features.Product.Requests.Queries
{
   public class GetProductQuery :IRequest<ProductDto>
    {
        public Guid Id { get; set; }
    }
}
