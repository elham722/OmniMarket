using OmniMarket.Common.Dtos.Product;

namespace OmniMarket.Application.Validators
{
   public class UpdateProductDtoValidator:AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator()
        {
            
            RuleFor(p => p.Id)
                .NotEqual(Guid.Empty).WithMessage("شناسه محصول معتبر نیست.");


            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("نام محصول نمی‌تواند خالی باشد.")
                .MinimumLength(3).WithMessage("نام محصول باید حداقل ۳ کاراکتر باشد.")
                .MaximumLength(100).WithMessage("نام محصول نمی‌تواند بیشتر از ۱۰۰ کاراکتر باشد.");

           
            RuleFor(p => p.Description)
                .MaximumLength(500).When(p => p.Description != null).WithMessage("توضیحات نمی‌تواند بیشتر از ۵۰۰ کاراکتر باشد.");


            RuleFor(p => p.Price)
                .GreaterThanOrEqualTo(0).When(p => p.Price.HasValue).WithMessage("قیمت نمی‌تواند منفی باشد.");


            RuleFor(p => p.Stock)
                .GreaterThanOrEqualTo(0).When(p => p.Stock.HasValue).WithMessage("موجودی نمی‌تواند منفی باشد.");

          
            RuleFor(p => p.ProductImages)
                .NotNull().WithMessage("لیست تصاویر نمی‌تواند نال باشد.");

            
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

           
            RuleFor(p => p.ProductImages)
                .Must(images => images.Count(i => i.IsPrimary) <= 1)
                .WithMessage("فقط یک تصویر می‌تواند به عنوان تصویر اصلی انتخاب شود.");
        }
    }
}
