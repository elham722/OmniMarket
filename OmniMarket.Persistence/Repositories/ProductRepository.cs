
using OmniMarket.Application.Contracts.Pagination;

namespace OmniMarket.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(OmniMarketDbContext context, IPaginationService paginationService) : base(context, paginationService)
        {
        }
    }
}
