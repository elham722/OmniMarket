using System.Linq;
using OmniMarket.Application.Contracts.Pagination;
using OmniMarket.Domain.Entities.Base;

namespace OmniMarket.Persistence.Services
{
    public class PaginationService : IPaginationService
    {
        public PagedList<T> CreatePagedList<T>(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}