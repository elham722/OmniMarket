namespace OmniMarket.Web.Models.Product
{
    public class CreateProductImageViewModel
    {
        [Required(ErrorMessage = "آدرس تصویر اجباری است.")]
        [Url(ErrorMessage = "آدرس تصویر باید یک URL معتبر باشد.")]
        public string Url { get; set; }

        public bool IsPrimary { get; set; }
    }
}
