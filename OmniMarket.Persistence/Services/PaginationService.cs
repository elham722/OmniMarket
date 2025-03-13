using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using OmniMarket.Application.Contracts.Pagination;

namespace OmniMarket.Persistence.Services
{
    public class PaginationService : IPaginationService
    {
        public async Task<PagedList<T>> CreatePagedListAsync<T>(
            IQueryable<T> source,
            int pageNumber,
            int pageSize,
            Expression<Func<T, object>>? orderBy = null,
            bool orderByDescending = false,
            Expression<Func<T, bool>>? filter = null,
            CancellationToken cancellationToken = default)
        {
            // اعتبارسنجی ورودی‌ها
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be greater than 0.");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than 0.");

            // اعمال فیلتر اگه وجود داشته باشه
            if (filter != null)
            {
                source = source.Where(filter);
            }

            // اعمال مرتب‌سازی اگه وجود داشته باشه
            if (orderBy != null)
            {
                source = orderByDescending ? source.OrderByDescending(orderBy) : source.OrderBy(orderBy);
            }

            // محاسبه تعداد کل آیتم‌ها
            var count = await source.CountAsync(cancellationToken);

            // گرفتن داده‌های صفحه‌بندی‌شده
            var items = await source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}