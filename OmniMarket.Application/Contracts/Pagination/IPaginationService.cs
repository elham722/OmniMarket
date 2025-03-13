
using System.Linq;

namespace OmniMarket.Application.Contracts.Pagination
{
    public interface IPaginationService
    {
        Task<PagedList<T>> CreatePagedListAsync<T>(
            IQueryable<T> source,
            int pageNumber,
            int pageSize,
            Expression<Func<T, object>>? orderBy = null,
            bool orderByDescending = false,
            Expression<Func<T, bool>>? filter = null,
            CancellationToken cancellationToken = default);
    }
}