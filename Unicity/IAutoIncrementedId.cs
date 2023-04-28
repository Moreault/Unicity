namespace ToolBX.Unicity;

public interface IAutoIncrementedId<out T> where T : struct, INumber<T>
{
    T Id { get; }
}