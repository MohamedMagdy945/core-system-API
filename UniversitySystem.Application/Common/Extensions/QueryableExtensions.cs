using System.Linq.Expressions;

namespace UniversitySystem.Application.Common.Extensions
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
        Expression<Func<T, TKey>>? keySelector, // خليناه يقبل null
        bool desc = false)
        {
            if (keySelector == null) return query; // لو مفيش ترتيب، كمل عادي

            return desc
                ? query.OrderByDescending(keySelector)
                : query.OrderBy(keySelector);
        }
    }
}
