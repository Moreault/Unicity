![Unicity](https://github.com/Moreault/Unicity/blob/master/unicity.png)

# Unicity
Extensions for auto-incrementing numbers in non-database settings.

## Major updates

As of 2.1.0, Unicity's methods use generic maths and therefore (should) support all base numeric types. This was previously limited to `System.Int32`.

## Gettings started

### With `IAutoIncrementedId<T>`

```c#
//The IAutoIncrementedId<T> interface forces your implementing types to have a T Id with a get accessor
public record YourClass : IAutoIncrementedId<int>
{
	public int Id { get; init; }
	...
}
```

```c#
//Will get the next available number from the _items collection or `0`
var yourObject = new YourClass 
{ 
	Id = _items.GetNextAvailableIdOrDefault();
	...
};
```

```c#
//Or if you don't like `0` as a `defaultValue`, you can set it to something else like `1`
var yourObject = new YourClass
{ 
	Id = _items.GetNextAvailableIdOrDefault(1);
	...
};
```

### Without `IAutoIncrementedId<T>`

Maybe you don't have control over `YourClass` or maybe you don't want to use the `IAutoIncrementedId<int>` interface. You can use the `GetNextAvailableIdOrDefault` method for those casse.

```c#
public record YourClass
{
	public int Number { get; init; }
	...
}
```

```c#
//This will give you the largest `Number` property in the collection `+ 1` or `0`
var yourOtherObject = new YourClassWithoutId
{
	Number = _otherItems.GetNextAvailableNumberOrDefault(x => x.Number)
}
```

```c#
//You can also specify a `defaultValue` if you don't like `0` (`5` in this case)
var yourOtherObject = new YourClassWithoutId
{
	Number = _otherItems.GetNextAvailableNumberOrDefault(x => x.Number, 5)
}
```

## Breaking changes

2.0.X -> 2.1.0

Method `GetNextAvailableId` is renamed to `GetNextAvailableIdOrDefault` which includes an optional `defaultValue` of `0`. Both methods are supported as of `2.0.2` to facilitate the move.