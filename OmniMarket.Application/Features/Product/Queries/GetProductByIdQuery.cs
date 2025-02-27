namespace OmniMarket.Application.Features.Product.Queries
{
   public class GetProductByIdQuery :IRequest<ProductDto>
    {
        public Guid Id { get; set; }
    }
}
