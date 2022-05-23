namespace ToolBX.Unicity;

public static class EnumerableExtensions
{
    public static int GetNextAvailableId(this IEnumerable<IAutoIncrementedId<int>> ids)
    {
        if (ids == null) throw new ArgumentNullException(nameof(ids));
        var maxId = ids.MaxBy(x => x.Id)?.Id ?? -1;
        if (maxId == int.MaxValue) throw new Exception(string.Format(Exceptions.CannotIncrementBecauseMaxValue, nameof(Int32), int.MaxValue));
        return maxId + 1;
    }

    public static bool ContainsDuplicateIds(this IEnumerable<IAutoIncrementedId<int>> ids)
    {
        if (ids == null) throw new ArgumentNullException(nameof(ids));
        return ids.GroupBy(x => x.Id).Where(x => x.Count() > 1).Select(x => x.Key).Any();
    }
}