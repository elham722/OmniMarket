namespace OmniMarket.Application.Models.Identity.Validators
{
    public class AuthRequestValidator : AbstractValidator<AuthRequest>
    {
        public AuthRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("ایمیل نمی‌تواند خالی باشد.")
                .EmailAddress().WithMessage("فرمت ایمیل اشتباه است.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("رمز عبور نمی‌تواند خالی باشد.");
        }
    }
}