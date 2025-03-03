// OmniMarket.UI/ViewModels/UpdateProductViewModel.cs
using System.ComponentModel.DataAnnotations;

namespace OmniMarket.UI.Models.Product
{
    public class UpdateProductViewModel : ProductFormViewModel
    {
        public List<UpdateProductImageViewModel> ProductImages { get; set; } = new List<UpdateProductImageViewModel>();
    }

    public class UpdateProductImageViewModel
    {
        public Guid Id { get; set; } // برای ویرایش، نیاز به Id تصویر داریم

        [Url(ErrorMessage = "آدرس تصویر باید یک URL معتبر باشد.")]
        public string Url { get; set; }

        public bool IsPrimary { get; set; }
    }
}