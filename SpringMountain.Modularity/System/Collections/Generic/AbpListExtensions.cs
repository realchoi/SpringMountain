namespace System.Collections.Generic;

/// <summary>
/// <see cref="IList{T}"/> 扩展方法
/// </summary>
public static class AbpListExtensions
{
    /// <summary>
    /// 从给定的集合的指定位置开始，往其中插入一个新的集合。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="index"></param>
    /// <param name="items"></param>
    public static void InsertRange<T>(this IList<T> source, int index, IEnumerable<T> items)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        foreach (var item in items)
        {
            source.Insert(index++, item);
        }
    }

    /// <summary>
    /// 从给定的集合中，查找第一个符合条件的元素的索引。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static int FindIndex<T>(this IList<T> source, Predicate<T> selector)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        for (var i = 0; i < source.Count; ++i)
        {
            if (selector(source[i]))
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// 往给定的集合的开头插入一个新的元素。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="item"></param>
    public static void AddFirst<T>(this IList<T> source, T item)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        source.Insert(0, item);
    }

    /// <summary>
    /// 往给定的集合的尾部插入一个新的元素。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="item"></param>
    public static void AddLast<T>(this IList<T> source, T item)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        source.Insert(source.Count, item);
    }

    /// <summary>
    /// 在给定的集合的特定元素后面，插入一个新的元素；如果集合中不存在特定的元素，则将新元素插在集合的开头。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="existingItem"></param>
    /// <param name="item"></param>
    public static void InsertAfter<T>(this IList<T> source, T existingItem, T item)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        var index = source.IndexOf(existingItem);
        if (index < 0)
        {
            source.AddFirst(item);
            return;
        }

        source.Insert(index + 1, item);
    }


    /// <summary>
    /// 在给定的集合的符合条件的元素后面，插入一个新的元素；如果集合中不存在符合条件的元素，则将新元素插在集合的开头。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <param name="item"></param>
    public static void InsertAfter<T>(this IList<T> source, Predicate<T> selector, T item)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        var index = source.FindIndex(selector);
        if (index < 0)
        {
            source.AddFirst(item);
            return;
        }

        source.Insert(index + 1, item);
    }

    /// <summary>
    /// 在给定的集合的特定元素前面，插入一个新的元素；如果集合中不存在特定的元素，则将新元素插在集合的尾部。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="existingItem"></param>
    /// <param name="item"></param>
    public static void InsertBefore<T>(this IList<T> source, T existingItem, T item)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        var index = source.IndexOf(existingItem);
        if (index < 0)
        {
            source.AddLast(item);
            return;
        }

        source.Insert(index, item);
    }

    /// <summary>
    /// 在给定的集合的符合条件的元素前面，插入一个新的元素；如果集合中不存在符合条件的元素，则将新元素插在集合的尾部。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <param name="item"></param>
    public static void InsertBefore<T>(this IList<T> source, Predicate<T> selector, T item)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        var index = source.FindIndex(selector);
        if (index < 0)
        {
            source.AddLast(item);
            return;
        }

        source.Insert(index, item);
    }

    /// <summary>
    /// 将给定的集合中所有符合条件的元素，替换为新的元素。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <param name="item"></param>
    public static void ReplaceWhile<T>(this IList<T> source, Predicate<T> selector, T item)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        for (int i = 0; i < source.Count; i++)
        {
            if (selector(source[i]))
            {
                source[i] = item;
            }
        }
    }

    /// <summary>
    /// 将给定的集合中所有符合条件的元素，替换为新的元素。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <param name="itemFactory"></param>
    public static void ReplaceWhile<T>(this IList<T> source, Predicate<T> selector, Func<T, T> itemFactory)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        for (int i = 0; i < source.Count; i++)
        {
            var item = source[i];
            if (selector(item))
            {
                source[i] = itemFactory(item);
            }
        }
    }

    /// <summary>
    /// 将给定的集合中第一个符合条件的元素，替换为新的元素。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <param name="item"></param>
    public static void ReplaceOne<T>(this IList<T> source, Predicate<T> selector, T item)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        for (int i = 0; i < source.Count; i++)
        {
            if (selector(source[i]))
            {
                source[i] = item;
                return;
            }
        }
    }

    /// <summary>
    /// 将给定的集合中第一个符合条件的元素，替换为新的元素。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <param name="itemFactory"></param>
    public static void ReplaceOne<T>(this IList<T> source, Predicate<T> selector, Func<T, T> itemFactory)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        for (int i = 0; i < source.Count; i++)
        {
            var item = source[i];
            if (selector(item))
            {
                source[i] = itemFactory(item);
                return;
            }
        }
    }

    /// <summary>
    /// 将给定的集合中第一个符合条件的元素，替换为新的元素。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="item"></param>
    /// <param name="replaceWith"></param>
    public static void ReplaceOne<T>(this IList<T> source, T item, T replaceWith)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        for (int i = 0; i < source.Count; i++)
        {
            if (Comparer<T>.Default.Compare(source[i], item) == 0)
            {
                source[i] = replaceWith;
                return;
            }
        }
    }

    public static void MoveItem<T>(this List<T> source, Predicate<T> selector, int targetIndex)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        if (!targetIndex.IsBetween(0, source.Count - 1))
        {
            throw new ArgumentOutOfRangeException("targetIndex 必须在 0 和 " + (source.Count - 1) + " 之间。");
        }

        var currentIndex = source.FindIndex(0, selector);
        if (currentIndex == targetIndex)
        {
            return;
        }

        var item = source[currentIndex];
        source.RemoveAt(currentIndex);
        source.Insert(targetIndex, item);
    }

    public static T GetOrAdd<T>(this IList<T> source, Func<T, bool> selector, Func<T> factory)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        var item = source.FirstOrDefault(selector);

        if (item == null)
        {
            item = factory();
            source.Add(item);
        }

        return item;
    }

    /// <summary>
    /// Sort a list by a topological sorting, which consider their dependencies.
    /// </summary>
    /// <typeparam name="T">The type of the members of values.</typeparam>
    /// <param name="source">A list of objects to sort</param>
    /// <param name="getDependencies">Function to resolve the dependencies</param>
    /// <param name="comparer">Equality comparer for dependencies </param>
    /// <returns>
    /// Returns a new list ordered by dependencies.
    /// If A depends on B, then B will come before than A in the resulting list.
    /// </returns>
    public static List<T> SortByDependencies<T>(
        this IEnumerable<T> source,
        Func<T, IEnumerable<T>> getDependencies,
        IEqualityComparer<T> comparer = null)
    {
        /* See: http://www.codeproject.com/Articles/869059/Topological-sorting-in-Csharp
         *      http://en.wikipedia.org/wiki/Topological_sorting
         */

        ArgumentNullException.ThrowIfNull(source, nameof(source));

        var sorted = new List<T>();
        var visited = new Dictionary<T, bool>(comparer);

        foreach (var item in source)
        {
            SortByDependenciesVisit(item, getDependencies, sorted, visited);
        }

        return sorted;
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T">The type of the members of values.</typeparam>
    /// <param name="item">Item to resolve</param>
    /// <param name="getDependencies">Function to resolve the dependencies</param>
    /// <param name="sorted">List with the sortet items</param>
    /// <param name="visited">Dictionary with the visited items</param>
    private static void SortByDependenciesVisit<T>(T item, Func<T, IEnumerable<T>> getDependencies, List<T> sorted,
        Dictionary<T, bool> visited)
    {
        bool inProcess;
        var alreadyVisited = visited.TryGetValue(item, out inProcess);

        if (alreadyVisited)
        {
            if (inProcess)
            {
                throw new ArgumentException("Cyclic dependency found! Item: " + item);
            }
        }
        else
        {
            visited[item] = true;

            var dependencies = getDependencies(item);
            if (dependencies != null)
            {
                foreach (var dependency in dependencies)
                {
                    SortByDependenciesVisit(dependency, getDependencies, sorted, visited);
                }
            }

            visited[item] = false;
            sorted.Add(item);
        }
    }
}