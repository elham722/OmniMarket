
namespace OmniMarket.UI.Models.Product
{
    public abstract class ProductFormViewModel
    {
        [Required(ErrorMessage = "نام محصول اجباری است.")]
        [StringLength(100, ErrorMessage = "نام محصول باید حداکثر ۱۰۰ کاراکتر باشد.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "توضیحات باید حداکثر ۵۰۰ کاراکتر باشد.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "قیمت اجباری است.")]
        [Range(0, double.MaxValue, ErrorMessage = "قیمت باید بزرگ‌تر یا مساوی صفر باشد.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "موجودی اجباری است.")]
        [Range(0, int.MaxValue, ErrorMessage = "موجودی باید بزرگ‌تر یا مساوی صفر باشد.")]
        public int Stock { get; set; }
    }
}