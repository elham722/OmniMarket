
namespace OmniMarket.Application.DTOs
{
   public class CategoryDto : BaseDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
