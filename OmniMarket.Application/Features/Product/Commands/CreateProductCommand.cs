
using OmniMarket.Application.Responses;

namespace OmniMarket.Application.Features.Product.Commands
{
   public class CreateProductCommand:IRequest<BaseCommandResponse>
    {
            public string Name { get; set; }
            public string? Description { get; set; }
            public decimal Price { get; set; }
            public int Stock { get; set; }
            public List<CreateProductImageDto> ProductImages { get; set; }
        
    }
}
