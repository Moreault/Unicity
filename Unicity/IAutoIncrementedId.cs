namespace ToolBX.Unicity;

public interface IAutoIncrementedId<out T> where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
{
    T Id { get; }
}