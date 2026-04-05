using System.Linq.Expressions;

namespace AppCoreSystem.Application.Common.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyPagination<T>(
        this IQueryable<T> source,
        int pageNumber, int pageSize)
        {
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;

            pageSize = pageSize <= 0 ? 10 : pageSize;

            pageSize = pageSize > 20 ? 20 : pageSize;

            return source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
        public static IQueryable<T> ApplySorting<T, TKey>(
        this IQueryable<T> query,
        Expression<Func<T, TKey>>? keySelector,
        bool desc = false)
        {
            if (keySelector == null) return query;

            return desc
                ? query.OrderByDescending(keySelector)
                : query.OrderBy(keySelector);
        }

        public static IQueryable<T> ApplyFilter<T>(
        this IQueryable<T> query,
        bool condition,
        Expression<Func<T, bool>> predicate)
        {
            return condition ? query.Where(predicate) : query;
        }
    }
}
