namespace Unicity.Tests;

[TestClass]
public class EnumerableExtensionsTester
{
    [TestClass]
    public class GetNextAvailableId : Tester
    {
        [TestMethod]
        public void WhenIdsIsNull_Throw()
        {
            //Arrange
            IEnumerable<Dummy> ids = null!;

            //Act
            var action = () => ids.GetNextAvailableId();

            //Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void WhenIdsEmpty_ReturnZero()
        {
            //Arrange
            var ids = Array.Empty<Dummy>();

            //Act
            var result = ids.GetNextAvailableId();

            //Assert
            result.Should().Be(0);
        }

        [TestMethod]
        public void WhenLastMaxIdIsMaxValue_Throw()
        {
            //Arrange
            var ids = new List<Dummy>
            {
                Fixture.Create<Dummy>(),
                new(int.MaxValue, Fixture.Create<string>()),
                Fixture.Create<Dummy>(),
                Fixture.Create<Dummy>(),
            };

            //Act
            var action = () => ids.GetNextAvailableId();

            //Assert
            action.Should().Throw<Exception>().WithMessage(string.Format(Exceptions.CannotIncrementBecauseMaxValue, nameof(Int32), int.MaxValue));
        }

        [TestMethod]
        public void WhenMaxIdIsBetweenZeroAndMaxValue_ReturnMaxIdPlusOne()
        {
            //Arrange
            var ids = Fixture.CreateMany<Dummy>().ToList();

            //Act
            var result = ids.GetNextAvailableId();

            //Assert
            result.Should().Be(ids.MaxBy(x => x.Id)!.Id + 1);
        }
    }

    [TestClass]
    public class ContainsDuplicateIds : Tester
    {
        [TestMethod]
        public void WhenIdsIsNull_Throw()
        {
            //Arrange
            IEnumerable<Dummy> ids = null!;

            //Act
            var action = () => ids.GetNextAvailableId();

            //Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void WhenIdsIsEmpty_ReturnFalse()
        {
            //Arrange
            var ids = Array.Empty<Dummy>();

            //Act
            var result = ids.ContainsDuplicateIds();

            //Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void WhenIdsContainsDuplicateIds_ReturnTrue()
        {
            //Arrange
            var id = Fixture.Create<int>();
            var ids = Fixture.Build<Dummy>().With(x => x.Id, id).CreateMany().ToList();

            //Act
            var result = ids.ContainsDuplicateIds();

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenIdsDoNotContainDuplicateIds_ReturnFalse()
        {
            //Arrange
            var ids = Fixture.CreateMany<Dummy>().ToList();

            //Act
            var result = ids.ContainsDuplicateIds();

            //Assert
            result.Should().BeFalse();
        }
    }

    [TestClass]
    public class GetNextAvailableId_Predicate : Tester
    {
        [TestMethod]
        public void WhenIdsIsNull_Throw()
        {
            //Arrange
            IEnumerable<NoInterfaceDummy> source = null!;
            var defaultValue = Fixture.Create<int>();

            //Act
            var action = () => source.GetNextAvailableNumberOrDefault(x => x.SomeNumber, defaultValue);

            //Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void WhenSourceEmpty_ReturnDefault()
        {
            //Arrange
            var ids = Array.Empty<NoInterfaceDummy>();
            var defaultValue = Fixture.Create<int>();

            //Act
            var result = ids.GetNextAvailableNumberOrDefault(x => x.SomeNumber, defaultValue);

            //Assert
            result.Should().Be(defaultValue);
        }

        [TestMethod]
        public void WhenHighestValueIsTypeMaxValue_Throw()
        {
            //Arrange
            var source = new List<NoInterfaceDummy>
            {
                Fixture.Create<NoInterfaceDummy>(),
                new(Fixture.Create<int>(), int.MaxValue, Fixture.Create<string>()),
                Fixture.Create<NoInterfaceDummy>(),
                Fixture.Create<NoInterfaceDummy>(),
            };
            var defaultValue = Fixture.Create<int>();

            //Act
            var action = () => source.GetNextAvailableNumberOrDefault(x => x.SomeNumber, defaultValue);

            //Assert
            action.Should().Throw<Exception>().WithMessage(string.Format(Exceptions.CannotIncrementBecauseMaxValue, nameof(Int32), int.MaxValue));
        }

        [TestMethod]
        public void WhenHighestValueIsBetweenZeroAndMaxValue_ReturnMaxValuePlusOne()
        {
            //Arrange
            var ids = Fixture.CreateMany<NoInterfaceDummy>().ToList();
            var defaultValue = Fixture.Create<int>();

            //Act
            var result = ids.GetNextAvailableNumberOrDefault(x => x.SomeNumber, defaultValue);

            //Assert
            result.Should().Be(ids.Max(x => x.SomeNumber) + 1);
        }
    }
}