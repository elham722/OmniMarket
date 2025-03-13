using OmniMarket.Application.Contracts.Pagination;

namespace OmniMarket.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly OmniMarketDbContext _context;
        private readonly DbSet<T> _entities;
        private readonly IPaginationService _paginationService;

        public GenericRepository(OmniMarketDbContext context, IPaginationService paginationService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _entities = _context.Set<T>();
            _paginationService = paginationService;
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _entities.AddAsync(entity, cancellationToken);
            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            if (entities == null || !entities.Any())
                throw new ArgumentNullException(nameof(entities));

            await _entities.AddRangeAsync(entities, cancellationToken);
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _entities.Remove(entity);
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            if (entities == null || !entities.Any())
                throw new ArgumentNullException(nameof(entities));

            _entities.RemoveRange(entities);
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _entities.AnyAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _entities.AnyAsync(predicate, cancellationToken);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _entities
                .Where(p => !p.IsDeleted).AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<T>> GetAllWithPredicateAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _entities
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync(cancellationToken);
        }

        public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _entities
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PagedList<T>> GetPagedAsync(
            int page,
            int pageSize,
            Expression<Func<T, object>>? orderBy = null,
            bool orderByDescending = false,
            Expression<Func<T, bool>>? filter = null,
            CancellationToken cancellationToken = default)
        {
            // شروع کوئری از دیتابیس
            IQueryable<T> query = _entities.AsNoTracking().Where(e => !e.IsDeleted);

            // واگذاری صفحه‌بندی به PaginationService
            return await _paginationService.CreatePagedListAsync(
                source: query,
                pageNumber: page,
                pageSize: pageSize,
                orderBy: orderBy,
                orderByDescending: orderByDescending,
                filter: filter,
                cancellationToken: cancellationToken);
        }
        public async Task<int> GetTotalCountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default)
        {
            if (predicate == null)
                return await _entities.CountAsync(cancellationToken);

            return await _entities
                .Where(predicate)
                .CountAsync(cancellationToken);
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _entities.Update(entity);
            return entity;
        }
    }
}