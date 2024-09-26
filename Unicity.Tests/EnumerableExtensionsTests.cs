namespace Unicity.Tests;

[TestClass]
public class EnumerableExtensionsTests : Tester
{
    [TestMethod]
    public void GetNextAvailableIdOrDefault_WhenIdsIsNull_Throw()
    {
        //Arrange
        IEnumerable<Garbage> ids = null!;

        //Act
        var action = () => ids.GetNextAvailableIdOrDefault();

        //Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void GetNextAvailableIdOrDefault_WhenIdsEmpty_ReturnZero()
    {
        //Arrange
        var ids = Array.Empty<Garbage>();

        //Act
        var result = ids.GetNextAvailableIdOrDefault();

        //Assert
        result.Should().Be(0);
    }

    [TestMethod]
    public void GetNextAvailableIdOrDefault_WhenLastMaxIdIsMaxValue_Throw()
    {
        //Arrange
        var ids = new List<Garbage>
        {
            Dummy.Create<Garbage>(),
            new(int.MaxValue, Dummy.Create<string>()),
            Dummy.Create<Garbage>(),
            Dummy.Create<Garbage>(),
        };

        //Act
        var action = () => ids.GetNextAvailableIdOrDefault();

        //Assert
        action.Should().Throw<Exception>().WithMessage(string.Format(Exceptions.CannotIncrementBecauseMaxValue, nameof(Int32), int.MaxValue));
    }

    [TestMethod]
    public void GetNextAvailableIdOrDefault_WhenMaxIdIsBetweenZeroAndMaxValue_ReturnMaxIdPlusOne()
    {
        //Arrange
        var ids = Dummy.CreateMany<Garbage>().ToList();

        //Act
        var result = ids.GetNextAvailableIdOrDefault();

        //Assert
        result.Should().Be(ids.MaxBy(x => x.Id)!.Id + 1);
    }

    [TestMethod]
    public void ContainsDuplicateIds_WhenIdsIsNull_Throw()
    {
        //Arrange
        IEnumerable<Garbage> ids = null!;

        //Act
        var action = () => ids.ContainsDuplicateIds();

        //Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void ContainsDuplicateIds_WhenIdsIsEmpty_ReturnFalse()
    {
        //Arrange
        var ids = Array.Empty<Garbage>();

        //Act
        var result = ids.ContainsDuplicateIds();

        //Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void ContainsDuplicateIds_WhenIdsContainsDuplicateIds_ReturnTrue()
    {
        //Arrange
        var id = Dummy.Create<int>();
        var ids = Dummy.Build<Garbage>().With(x => x.Id, id).CreateMany().ToList();

        //Act
        var result = ids.ContainsDuplicateIds();

        //Assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void ContainsDuplicateIds_WhenIdsDoNotContainDuplicateIds_ReturnFalse()
    {
        //Arrange
        var ids = Dummy.CreateMany<Garbage>().ToList();

        //Act
        var result = ids.ContainsDuplicateIds();

        //Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void GetNextAvailableIdPredicate_WhenSourceIsNull_Throw()
    {
        //Arrange
        IEnumerable<NoInterfaceDummy> source = null!;
        var defaultValue = Dummy.Create<int>();

        //Act
        var action = () => source.GetNextAvailableNumberOrDefault(x => x.SomeNumber, defaultValue);

        //Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void GetNextAvailableIdPredicate_WhenSelectorIsNull_Throw()
    {
        //Arrange
        var source = Dummy.CreateMany<NoInterfaceDummy>().ToList();
        var defaultValue = Dummy.Create<int>();

        //Act
        var action = () => source.GetNextAvailableNumberOrDefault(null!, defaultValue);

        //Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void GetNextAvailableIdPredicate_WhenSourceEmpty_ReturnDefault()
    {
        //Arrange
        var ids = Array.Empty<NoInterfaceDummy>();
        var defaultValue = Dummy.Create<int>();

        //Act
        var result = ids.GetNextAvailableNumberOrDefault(x => x.SomeNumber, defaultValue);

        //Assert
        result.Should().Be(defaultValue);
    }

    [TestMethod]
    public void GetNextAvailableIdPredicate_WhenHighestValueIsTypeMaxValue_Throw()
    {
        //Arrange
        var source = new List<NoInterfaceDummy>
            {
                Dummy.Create<NoInterfaceDummy>(),
                new(Dummy.Create<int>(), int.MaxValue, Dummy.Create<string>()),
                Dummy.Create<NoInterfaceDummy>(),
                Dummy.Create<NoInterfaceDummy>(),
            };
        var defaultValue = Dummy.Create<int>();

        //Act
        var action = () => source.GetNextAvailableNumberOrDefault(x => x.SomeNumber, defaultValue);

        //Assert
        action.Should().ThrowExactly<NumberIncrementationException<int>>().WithMessage(string.Format(Exceptions.CannotIncrementBecauseMaxValue, nameof(Int32), int.MaxValue));
    }

    [TestMethod]
    public void GetNextAvailableIdPredicate_WhenHighestValueIsBetweenZeroAndMaxValue_ReturnMaxValuePlusOne()
    {
        //Arrange
        var ids = Dummy.CreateMany<NoInterfaceDummy>().ToList();
        var defaultValue = Dummy.Create<int>();

        //Act
        var result = ids.GetNextAvailableNumberOrDefault(x => x.SomeNumber, defaultValue);

        //Assert
        result.Should().Be(ids.Max(x => x.SomeNumber) + 1);
    }
}