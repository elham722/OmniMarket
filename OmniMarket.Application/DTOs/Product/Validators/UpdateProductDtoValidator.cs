
namespace OmniMarket.Application.DTOs.Product.Validators
{
   public class UpdateProductDtoValidator:AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator()
        {
            // Id باید معتبر باشه (نمی‌تونه Guid.Empty باشه)
            RuleFor(p => p.Id)
                .NotEqual(Guid.Empty).WithMessage("شناسه محصول معتبر نیست.");

            // Name فقط وقتی مقدار داره چک بشه
            RuleFor(p => p.Name)
                .NotEmpty().When(p => p.Name != null).WithMessage("نام محصول نمی‌تواند خالی باشد.")
                .MaximumLength(100).When(p => p.Name != null).WithMessage("نام محصول نمی‌تواند بیشتر از ۱۰۰ کاراکتر باشد.");

            // Description فقط وقتی مقدار داره چک بشه
            RuleFor(p => p.Description)
                .MaximumLength(500).When(p => p.Description != null).WithMessage("توضیحات نمی‌تواند بیشتر از ۵۰۰ کاراکتر باشد.");

            // Price فقط وقتی مقدار داره چک بشه
            RuleFor(p => p.Price)
                .GreaterThanOrEqualTo(0).When(p => p.Price.HasValue).WithMessage("قیمت نمی‌تواند منفی باشد.");

            // Stock فقط وقتی مقدار داره چک بشه
            RuleFor(p => p.Stock)
                .GreaterThanOrEqualTo(0).When(p => p.Stock.HasValue).WithMessage("موجودی نمی‌تواند منفی باشد.");

            // ProductImages نباید null باشه (که هیچ‌وقت نیست)
            RuleFor(p => p.ProductImages)
                .NotNull().WithMessage("لیست تصاویر نمی‌تواند نال باشد.");

            // قوانین برای هر تصویر
            RuleForEach(p => p.ProductImages)
                .ChildRules(image =>
                {
                    image.RuleFor(i => i.Id)
                        .NotEqual(Guid.Empty).WithMessage("شناسه تصویر معتبر نیست.");

                    image.RuleFor(i => i.Url)
                        .NotEmpty().WithMessage("آدرس تصویر اجباری است.")
                        .MaximumLength(500).WithMessage("آدرس تصویر نمی‌تواند بیشتر از ۵۰۰ کاراکتر باشد.");

                    image.RuleFor(i => i.IsPrimary)
                        .NotNull().WithMessage("فیلد IsPrimary اجباری است.");
                });

            // فقط یه تصویر می‌تونه IsPrimary باشه
            RuleFor(p => p.ProductImages)
                .Must(images => images.Count(i => i.IsPrimary) <= 1)
                .WithMessage("فقط یک تصویر می‌تواند به عنوان تصویر اصلی انتخاب شود.");
        }
    }
}
