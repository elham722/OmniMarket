namespace OmniMarket.Persistence.DependencyInjection
{
    public static class PersistenceServicesConfig
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("OmniMarketConnectionString");
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString), "Connection string 'OmniMarketConnectionString' is not found in configuration.");

            services.AddDbContext<OmniMarketDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            #region Repository

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            #endregion

            return services;
        }
    }
}