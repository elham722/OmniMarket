using OmniMarket.Application.Responses;

namespace OmniMarket.Application.Features.Product.Commands
{
    public class DeleteProductCommandHandler(IProductRepository productRepository) : IRequestHandler<DeleteProductCommand, BaseCommandResponse>
    {
        

        public async Task<BaseCommandResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            // چک کردن وجود محصول
            var product = await productRepository.GetByIdAsync(request.Id,cancellationToken);
            if (product is null)
            {
                response.Success = false;
                response.Message = $"محصول با شناسه {request.Id} پیدا نشد.";
                response.Errors = new List<string> { "محصول وجود ندارد." };
                return response;
            }

            // حذف محصول
            await productRepository.DeleteAsync(product, cancellationToken);

            response.Success = true;
            response.Message = "محصول با موفقیت حذف شد.";
            response.Id = product.Id;

            return response;
        }
    }
}