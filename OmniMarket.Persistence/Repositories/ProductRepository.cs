using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmniMarket.Application.Persistence.Contracts;
using OmniMarket.Domain.Entities;
using OmniMarket.Persistence.Context;

namespace OmniMarket.Persistence.Repositories
{
   public class ProductRepository :GenericRepository<Product>,IProductRepository
   { 
       private readonly OmniMarketDbContext _context;
        public ProductRepository(OmniMarketDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
