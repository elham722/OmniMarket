
namespace OmniMarket.UI.Models.Product
{
    public class CreateProductViewModel : ProductFormViewModel
    {
        public List<CreateProductImageViewModel> ProductImages { get; set; } = new List<CreateProductImageViewModel>();
    }

   
}