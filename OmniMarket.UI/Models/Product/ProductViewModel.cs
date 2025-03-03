namespace OmniMarket.UI.Models.Product
{
    public class ProductViewModel: ProductFormViewModel
    {
        public Guid Id { get; set; }
        public List<ProductImageViewModel>? ProductImages { get; set; }
    }
}
