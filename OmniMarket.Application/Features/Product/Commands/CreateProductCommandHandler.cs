using OmniMarket.Application.Responses;

namespace OmniMarket.Application.Features.Product.Commands
{
    public class CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<CreateProductCommand, BaseCommandResponse>
    {

        public async Task<BaseCommandResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            // اعتبارسنجی
            var validator = new CreateProductCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "ایجاد محصول ناموفق بود.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            // ایجاد محصول
            var product = mapper.Map<Domain.Entities.Product>(request);
            product = await productRepository.AddAsync(product, cancellationToken);

            response.Success = true;
            response.Message = "محصول با موفقیت ایجاد شد.";
            response.Id = product.Id;

            return response;
        }
    }
}