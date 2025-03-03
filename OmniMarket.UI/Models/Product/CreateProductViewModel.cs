// OmniMarket.UI/ViewModels/CreateProductViewModel.cs
using System.ComponentModel.DataAnnotations;

namespace OmniMarket.UI.Models.Product
{
    public class CreateProductViewModel : ProductFormViewModel
    {
        public List<CreateProductImageViewModel> ProductImages { get; set; } = new List<CreateProductImageViewModel>();
    }

   
}