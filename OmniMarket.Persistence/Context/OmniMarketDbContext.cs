using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OmniMarket.Domain.Entities;

namespace OmniMarket.Persistence.Context
{
    public class OmniMarketDbContext:DbContext
    {
        public OmniMarketDbContext(DbContextOptions<OmniMarketDbContext> options):base(options)
        {
            
        }


        public DbSet<Product> Products { get; set; }
    }
}
