
using OmniMarket.Application.Features.Product.Requests.Queries;
using OmniMarket.Application.Persistence.Contracts;

namespace OmniMarket.Application.Features.Product.Handlers.Queries
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            return _mapper.Map<ProductDto>(product);
        }
    }
}
