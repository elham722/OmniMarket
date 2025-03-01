
namespace OmniMarket.Application.Features.Product.Validators
{
   public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEqual(Guid.Empty).WithMessage("شناسه محصول معتبر نیست.");
        }
    }
}
