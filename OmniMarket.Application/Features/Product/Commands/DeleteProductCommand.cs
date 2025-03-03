
using OmniMarket.Application.Responses;

namespace OmniMarket.Application.Features.Product.Commands
{
   public class DeleteProductCommand:IRequest<BaseCommandResponse>
    {
        public Guid Id { get; set; }
    }
}
