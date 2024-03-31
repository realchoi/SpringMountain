using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace System.Linq;

/// <summary>
/// <see cref="IQueryable{T}"/> 扩展方法
/// </summary>
public static class AbpQueryableExtensions
{
    /// <summary>
    /// Used for paging. Can be used as an alternative to Skip(...).Take(...) chaining.
    /// </summary>
    public static IQueryable<T> PageBy<T>([NotNull] this IQueryable<T> query, int skipCount, int maxResultCount)
    {
        ArgumentNullException.ThrowIfNull(query);

        return query.Skip(skipCount).Take(maxResultCount);
    }

    /// <summary>
    /// Used for paging. Can be used as an alternative to Skip(...).Take(...) chaining.
    /// </summary>
    public static TQueryable PageBy<T, TQueryable>([NotNull] this TQueryable query, int skipCount, int maxResultCount)
        where TQueryable : IQueryable<T>
    {
        ArgumentNullException.ThrowIfNull(query);

        return (TQueryable)query.Skip(skipCount).Take(maxResultCount);
    }

    /// <summary>
    /// Filters a <see cref="IQueryable{T}"/> by given predicate if given condition is true.
    /// </summary>
    /// <param name="query">Queryable to apply filtering</param>
    /// <param name="condition">A boolean value</param>
    /// <param name="predicate">Predicate to filter the query</param>
    /// <returns>Filtered or not filtered query based on <paramref name="condition"/></returns>
    public static IQueryable<T> WhereIf<T>([NotNull] this IQueryable<T> query, bool condition,
        Expression<Func<T, bool>> predicate)
    {
        ArgumentNullException.ThrowIfNull(query);

        return condition
            ? query.Where(predicate)
            : query;
    }

    /// <summary>
    /// Filters a <see cref="IQueryable{T}"/> by given predicate if given condition is true.
    /// </summary>
    /// <param name="query">Queryable to apply filtering</param>
    /// <param name="condition">A boolean value</param>
    /// <param name="predicate">Predicate to filter the query</param>
    /// <returns>Filtered or not filtered query based on <paramref name="condition"/></returns>
    public static TQueryable WhereIf<T, TQueryable>([NotNull] this TQueryable query, bool condition,
        Expression<Func<T, bool>> predicate)
        where TQueryable : IQueryable<T>
    {
        ArgumentNullException.ThrowIfNull(query);

        return condition
            ? (TQueryable)query.Where(predicate)
            : query;
    }

    /// <summary>
    /// Filters a <see cref="IQueryable{T}"/> by given predicate if given condition is true.
    /// </summary>
    /// <param name="query">Queryable to apply filtering</param>
    /// <param name="condition">A boolean value</param>
    /// <param name="predicate">Predicate to filter the query</param>
    /// <returns>Filtered or not filtered query based on <paramref name="condition"/></returns>
    public static IQueryable<T> WhereIf<T>([NotNull] this IQueryable<T> query, bool condition,
        Expression<Func<T, int, bool>> predicate)
    {
        ArgumentNullException.ThrowIfNull(query);

        return condition
            ? query.Where(predicate)
            : query;
    }

    /// <summary>
    /// Filters a <see cref="IQueryable{T}"/> by given predicate if given condition is true.
    /// </summary>
    /// <param name="query">Queryable to apply filtering</param>
    /// <param name="condition">A boolean value</param>
    /// <param name="predicate">Predicate to filter the query</param>
    /// <returns>Filtered or not filtered query based on <paramref name="condition"/></returns>
    public static TQueryable WhereIf<T, TQueryable>([NotNull] this TQueryable query, bool condition,
        Expression<Func<T, int, bool>> predicate)
        where TQueryable : IQueryable<T>
    {
        ArgumentNullException.ThrowIfNull(query);

        return condition
            ? (TQueryable)query.Where(predicate)
            : query;
    }


    /// <summary>
    /// Performs a subsequent ordering of the elements in a sequence in ascending order according to a key if given condition is true.
    /// </summary>
    /// <param name="orderedQuery">Queryable to apply filtering</param>
    /// <param name="condition">A boolean value</param>
    /// <param name="keySelector">Predicate to filter the query</param>
    /// <returns>An <see cref="IOrderedEnumerable{TSource}"/> whose elements are sorted according to a key. </returns>
    public static IOrderedQueryable<TSource> ThenByIf<TSource, TKey>(
        [NotNull] this IOrderedQueryable<TSource> orderedQuery, bool condition,
        Expression<Func<TSource, TKey>> keySelector)
    {
        ArgumentNullException.ThrowIfNull(orderedQuery);

        return condition
            ? orderedQuery.ThenBy(keySelector)
            : orderedQuery;
    }
}