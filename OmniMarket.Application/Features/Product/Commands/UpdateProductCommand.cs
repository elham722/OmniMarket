
namespace OmniMarket.Application.Features.Product.Commands
{
   public class UpdateProductCommand:IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
        public List<UpdateProductImageDto> ProductImages { get; set; } = new List<UpdateProductImageDto>();
    }
}
