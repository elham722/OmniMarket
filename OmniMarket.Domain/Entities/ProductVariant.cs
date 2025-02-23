
namespace OmniMarket.Domain.Entities
{
  public class ProductVariant
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public string VariantName { get; set; }
        public decimal PriceAdjustment { get; set; }

    }
}
