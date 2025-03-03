using OmniMarket.Application.Responses;

namespace OmniMarket.Application.Features.Product.Commands
{
    public class UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<UpdateProductCommand, BaseCommandResponse>
    {

        public async Task<BaseCommandResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            // اعتبارسنجی
            var validator = new UpdateProductCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "ویرایش محصول ناموفق بود.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            // ویرایش محصول
            var product = await productRepository.GetByIdAsync(request.Id,cancellationToken);
            if (product is null)
            {
                response.Success = false;
                response.Message = $"محصول با شناسه {request.Id} پیدا نشد.";
                response.Errors = new List<string> { "محصول وجود ندارد." };
                return response;
            }

            mapper.Map(request, product);
            await productRepository.UpdateAsync(product, cancellationToken);

            response.Success = true;
            response.Message = "محصول با موفقیت ویرایش شد.";
            response.Id = product.Id;

            return response;
        }
    }
}