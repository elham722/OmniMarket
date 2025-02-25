
using OmniMarket.Domain.Entities.Common;

namespace OmniMarket.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
