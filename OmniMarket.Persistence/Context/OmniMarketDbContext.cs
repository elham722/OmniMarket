namespace OmniMarket.Persistence.Context
{
    public class OmniMarketDbContext(DbContextOptions<OmniMarketDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OmniMarketDbContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreateDate = DateTime.UtcNow;
                   
                    if (entry.Entity.CreatedById == Guid.Empty)
                    {
                        entry.Entity.CreatedById = Guid.Parse("00000000-0000-0000-0000-000000000001");
                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdateDate = DateTime.UtcNow;
                  
                    entry.Entity.UpdatedById = Guid.Parse("00000000-0000-0000-0000-000000000001");
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.DeleteDate = DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreateDate = DateTime.UtcNow;
                    if (entry.Entity.CreatedById == Guid.Empty)
                    {
                        entry.Entity.CreatedById = Guid.Parse("00000000-0000-0000-0000-000000000001");
                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdateDate = DateTime.UtcNow;
                    entry.Entity.UpdatedById = Guid.Parse("00000000-0000-0000-0000-000000000001");
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.DeleteDate = DateTime.UtcNow;
                }
            }
            return base.SaveChanges();
        }
    }
}