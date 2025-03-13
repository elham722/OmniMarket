using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniMarket.Application.Features.Pagination.Queries
{
    public class GetPagedDataQuery<T> : IRequest<PagedList<T>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IQueryable<T> Query { get; set; }

        public GetPagedDataQuery(IQueryable<T> query, int pageNumber, int pageSize)
        {
            Query = query;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
