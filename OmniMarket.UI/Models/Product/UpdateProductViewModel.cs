namespace OmniMarket.Web.Models.Product
{
    public class UpdateProductViewModel : ProductFormViewModel
    {
        public Guid Id { get; set; }
        public List<UpdateProductImageViewModel> ProductImages { get; set; } = new List<UpdateProductImageViewModel>();
    }

    public class UpdateProductImageViewModel
    {
        public Guid Id { get; set; } 

        [Url(ErrorMessage = "آدرس تصویر باید یک URL معتبر باشد.")]
        public string Url { get; set; }

        public bool IsPrimary { get; set; }
    }
}