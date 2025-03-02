
namespace OmniMarket.Persistence.Repositories
{
    public class ProductRepository(OmniMarketDbContext context) : GenericRepository<Product>(context), IProductRepository
    {
        
    }
}
