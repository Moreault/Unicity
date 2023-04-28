namespace ToolBX.Unicity;

public static class EnumerableExtensions
{
    [Obsolete("Use GetNextAvailableIdOrDefault instead. Will be removed in 2.1.0")]
    public static int GetNextAvailableId(this IEnumerable<IAutoIncrementedId<int>> source) => source.GetNextAvailableNumberOrDefault(x => x.Id);

    public static int GetNextAvailableIdOrDefault(this IEnumerable<IAutoIncrementedId<int>> source, int defaultValue = 0) => source.GetNextAvailableNumberOrDefault(x => x.Id, defaultValue);

    public static bool ContainsDuplicateIds(this IEnumerable<IAutoIncrementedId<int>> source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        return source.GroupBy(x => x.Id).Where(x => x.Count() > 1).Select(x => x.Key).Any();
    }

    public static int GetNextAvailableNumberOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector, int defaultValue = 0)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (selector == null) throw new ArgumentNullException(nameof(selector));

        var maxId = source.Max(selector);
        return maxId switch
        {
            null => defaultValue,
            int.MaxValue => throw new Exception(string.Format(Exceptions.CannotIncrementBecauseMaxValue, nameof(Int32), int.MaxValue)),
            _ => maxId.Value + 1
        };
    }
}