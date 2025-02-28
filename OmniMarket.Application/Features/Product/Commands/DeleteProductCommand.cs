
namespace OmniMarket.Application.Features.Product.Commands
{
   public class DeleteProductCommand:IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
