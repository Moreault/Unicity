namespace ToolBX.Unicity;

public sealed class NumberIncrementationException<TNumber> : Exception where TNumber : struct, INumber<TNumber>, IMinMaxValue<TNumber>
{
    public NumberIncrementationException() : base(string.Format(Exceptions.CannotIncrementBecauseMaxValue, typeof(TNumber).Name, TNumber.MaxValue))
    {
    }
}