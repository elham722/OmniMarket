
namespace OmniMarket.Application.DTOs.ProductImage
{
    public class UpdateProductImageDto 
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public bool IsPrimary { get; set; }
    }

}
