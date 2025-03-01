
namespace OmniMarket.Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OmniMarketDbContext>(options =>
            {
                options.UseSqlServer(configuration
                    .GetConnectionString("OmniMarketConnectionString"));
            });


            #region Repository

           // services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
           // services.AddScoped<IProductRepository, ProductRepository>();

            #endregion

            return services;
        }

    }
}
