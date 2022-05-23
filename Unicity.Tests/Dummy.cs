namespace Unicity.Tests;

public record Dummy(int Id, string Name) : IAutoIncrementedId<int>;