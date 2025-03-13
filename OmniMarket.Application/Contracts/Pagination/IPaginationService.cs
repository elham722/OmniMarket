
using System.Linq;
using OmniMarket.Domain.Entities.Base;

namespace OmniMarket.Application.Contracts.Pagination
{
    public interface IPaginationService
    {
        PagedList<T> CreatePagedList<T>(IQueryable<T> source, int pageNumber, int pageSize);
    }
}