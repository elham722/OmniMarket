
namespace OmniMarket.Application.Models.Identity.Validators
{
    public class RegisterationRequestValidator : AbstractValidator<RegisterationRequest>
    {
        public RegisterationRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("نام نمی‌تواند خالی باشد.")
                .MaximumLength(50).WithMessage("نام نمی‌تواند بیشتر از ۵۰ کاراکتر باشد.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("نام خانوادگی نمی‌تواند خالی باشد.")
                .MaximumLength(50).WithMessage("نام خانوادگی نمی‌تواند بیشتر از ۵۰ کاراکتر باشد.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("ایمیل نمی‌تواند خالی باشد.")
                .EmailAddress().WithMessage("فرمت ایمیل اشتباه است.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("نام کاربری نمی‌تواند خالی باشد.")
                .MinimumLength(6).WithMessage("نام کاربری باید حداقل ۶ کاراکتر باشد.")
                .Matches("^[a-zA-Z0-9_]*$").WithMessage("نام کاربری فقط می‌تواند شامل حروف، اعداد و خط تیره باشد.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("رمز عبور نمی‌تواند خالی باشد.")
                .MinimumLength(6).WithMessage("رمز عبور باید حداقل ۶ کاراکتر باشد.")
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d).+$").WithMessage("رمز عبور باید حداقل یک حرف و یک عدد داشته باشد.");
        }
    }
}