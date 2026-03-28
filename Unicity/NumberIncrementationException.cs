namespace ToolBX.Unicity;

public sealed class NumberIncrementationException : Exception
{
    public NumberIncrementationException(string typeName, object maxValue)
        : base(string.Format(ExceptionMessages.CannotIncrementBecauseMaxValue, typeName, maxValue))
    {
    }
}
