

namespace OmniMarket.Application.Persistence.Contracts
{
   public interface IGenericRepository<T> where T :class
   {
       Task<T> GetByIdAsync(int id);
       Task<IReadOnlyList<T>> GetAllAsync();
       Task<T> AddAsync(T entity);
       Task<T> UpdateAsync(T entity);
       Task<T> DeleteAsync(int id);
   }
}
