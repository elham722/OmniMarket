namespace OmniMarket.Web.Models.Product
{
    public class ProductViewModel: ProductFormViewModel
    {
        public Guid Id { get; set; }
        public List<ProductImageViewModel>? ProductImages { get; set; }
    }
}
