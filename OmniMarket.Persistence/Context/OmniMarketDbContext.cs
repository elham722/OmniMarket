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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OmniMarketDbContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                entry.Entity.UpdateDate = DateTime.Now;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreateDate = DateTime.Now;
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                entry.Entity.UpdateDate=DateTime.Now;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreateDate=DateTime.Now;
                }
            }
            return base.SaveChanges();
        }
    }
}
