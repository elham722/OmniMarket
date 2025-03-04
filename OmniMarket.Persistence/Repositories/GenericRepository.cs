namespace OmniMarket.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly OmniMarketDbContext _context;
        private readonly DbSet<T> _entities;

        public GenericRepository(OmniMarketDbContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _entities.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            if (entities == null || !entities.Any())
                throw new ArgumentNullException(nameof(entities));

            await _entities.AddRangeAsync(entities, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _entities.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            if (entities == null || !entities.Any())
                throw new ArgumentNullException(nameof(entities));

            _entities.RemoveRange(entities);
            await _context.SaveChangesAsync(cancellationToken);
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

        public async Task<IReadOnlyList<T>> GetPagedAsync(int page, int pageSize, Expression<Func<T, object>>? orderBy = null, CancellationToken cancellationToken = default)
        {
            if (page <= 0)
                throw new ArgumentOutOfRangeException(nameof(page), "Page number must be greater than 0.");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than 0.");

            var query = _entities.AsNoTracking();

            if (orderBy != null)
            {
                query = query.OrderBy(orderBy);
            }

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
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
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }
    }
}