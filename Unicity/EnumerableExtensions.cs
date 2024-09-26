namespace ToolBX.Unicity;

public static class EnumerableExtensions
{
    /// <summary>
    /// Returns the next available id for the given source.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="NumberIncrementationException{TNumber}"/>
    public static TNumber GetNextAvailableIdOrDefault<TNumber>(this IEnumerable<IAutoIncrementedId<TNumber>> source, TNumber defaultValue = default) where TNumber : struct, INumber<TNumber>, IMinMaxValue<TNumber>
    {
        return source.GetNextAvailableNumberOrDefault(x => x.Id, defaultValue);
    }

    /// <summary>
    /// Returns true if the given source contains duplicate ids.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool ContainsDuplicateIds<TNumber>(this IEnumerable<IAutoIncrementedId<TNumber>> source) where TNumber : struct, INumber<TNumber>
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        return source.GroupBy(x => x.Id).Where(x => x.Count() > 1).Select(x => x.Key).Any();
    }

    /// <summary>
    /// Returns the next available number for the given source and selector.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="NumberIncrementationException{TNumber}"/>
    public static TNumber GetNextAvailableNumberOrDefault<TSource, TNumber>(this IEnumerable<TSource> source, Func<TSource, TNumber?> selector, TNumber defaultValue = default) where TNumber : struct, INumber<TNumber>, IMinMaxValue<TNumber>
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (selector == null) throw new ArgumentNullException(nameof(selector));

        var maxId = source.Max(selector);
        if (maxId == null) return defaultValue;
        if (maxId.Value == TNumber.MaxValue) throw new NumberIncrementationException<TNumber>();
        return maxId.Value + TNumber.One;
    }
}