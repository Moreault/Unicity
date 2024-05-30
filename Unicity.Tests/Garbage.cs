namespace Unicity.Tests;

public record Garbage(int Id, string Name) : IAutoIncrementedId<int>;