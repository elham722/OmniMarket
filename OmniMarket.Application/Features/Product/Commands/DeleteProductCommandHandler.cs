﻿
namespace OmniMarket.Application.Features.Product.Commands
{
    public class DeleteProductCommandHandler(IProductRepository productRepository) : IRequestHandler<DeleteProductCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
           var product= await productRepository.GetByIdAsync(request.Id, cancellationToken);

          if (product == null)
            throw new NotFoundException(nameof(Domain.Entities.Product), request.Id);

          await productRepository.DeleteAsync(product,cancellationToken);
           return Unit.Value;
        }
    }
}
