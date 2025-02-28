
namespace OmniMarket.Application.DTOs.ProductImage
{
    public class ProductImageDto : BaseDto
    {
        public Guid ProductId { get; set; }
        public string Url { get; set; }
        public bool IsPrimary { get; set; }
    }
}
