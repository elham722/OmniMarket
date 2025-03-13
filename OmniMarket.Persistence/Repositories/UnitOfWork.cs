using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using OmniMarket.Application.Contracts.Pagination; 

namespace OmniMarket.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OmniMarketDbContext _context;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(OmniMarketDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            return _serviceProvider.GetRequiredService<IGenericRepository<T>>();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}