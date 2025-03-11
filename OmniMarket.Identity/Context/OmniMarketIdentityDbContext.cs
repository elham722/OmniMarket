
namespace OmniMarket.Identity.Context
{
    public class OmniMarketIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public OmniMarketIdentityDbContext(DbContextOptions<OmniMarketIdentityDbContext> options)
        :base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
    }
}
