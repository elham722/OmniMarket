

namespace OmniMarket.Application.Persistence.Contracts
{
   public interface IGenericRepository<T> where T :class
   {
       Task<T> GetByIdAsync(Guid id);
       Task<IReadOnlyList<T>> GetAllAsync();
       Task<T> AddAsync(T entity);
       Task UpdateAsync(T entity);
       Task DeleteAsync(T entity);
       Task<bool> Exist(Guid id);
   }
}
