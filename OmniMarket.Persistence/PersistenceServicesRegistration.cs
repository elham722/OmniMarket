using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OmniMarket.Application.Persistence.Contracts;
using OmniMarket.Persistence.Context;
using OmniMarket.Persistence.Repositories;

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

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();

            #endregion

            return services;
        }

    }
}
