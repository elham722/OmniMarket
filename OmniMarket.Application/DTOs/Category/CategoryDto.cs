
namespace OmniMarket.Application.DTOs.Category
{
   public class CategoryDto : BaseDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
