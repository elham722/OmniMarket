
namespace OmniMarket.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(OmniMarketDbContext context) : base(context)
        {
        }
    }
}
