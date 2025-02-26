
namespace OmniMarket.Application.Contracts.Persistence
{
   public interface IGenericRepository<T> where T :BaseEntity
   {
       Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
       Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);
       Task<IReadOnlyList<T>> GetAllWithPredicateAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
       Task<IReadOnlyList<T>> GetPagedAsync(int page, int pageSize, Expression<Func<T, object>>? orderBy = null, CancellationToken cancellationToken = default);
       Task<int> GetTotalCountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default);

       Task AddAsync(T entity, CancellationToken cancellationToken = default);
       Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
       Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
       Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
       Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

       Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
       Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    }
}

