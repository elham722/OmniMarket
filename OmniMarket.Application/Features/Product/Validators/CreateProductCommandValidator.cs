
namespace OmniMarket.Application.Features.Product.Validators
{
   public class CreateProductCommandValidator:AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("نام محصول اجباری است.")
                .MaximumLength(100).WithMessage("نام محصول نمی‌تواند بیشتر از ۱۰۰ کاراکتر باشد.");

            RuleFor(p => p.Description)
                .MaximumLength(500).WithMessage("توضیحات نمی‌تواند بیشتر از ۵۰۰ کاراکتر باشد.");

            RuleFor(p => p.Price)
                .NotNull().WithMessage("قیمت اجباری است.")
                .GreaterThanOrEqualTo(0).WithMessage("قیمت نمی‌تواند منفی باشد.");

            RuleFor(p => p.Stock)
                .NotNull().WithMessage("موجودی اجباری است.")
                .GreaterThanOrEqualTo(0).WithMessage("موجودی نمی‌تواند منفی باشد.");

            RuleFor(p => p.ProductImages)
                .NotNull().WithMessage("لیست تصاویر نمی‌تواند نال باشد."); 

            RuleForEach(p => p.ProductImages)
                .ChildRules(image =>
                {
                    image.RuleFor(i => i.Url)
                        .NotEmpty().WithMessage("آدرس تصویر اجباری است.")
                        .MaximumLength(500).WithMessage("آدرس تصویر نمی‌تواند بیشتر از ۵۰۰ کاراکتر باشد.");

                    image.RuleFor(i => i.IsPrimary)
                        .NotNull().WithMessage("فیلد IsPrimary اجباری است.");
                });

            RuleFor(p => p.ProductImages)
                .Must(images => images.Count(i => i.IsPrimary) <= 1)
                .WithMessage("فقط یک تصویر می‌تواند به عنوان تصویر اصلی انتخاب شود.");
        }
    }
}
