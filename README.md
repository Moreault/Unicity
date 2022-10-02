![Unicity](https://github.com/Moreault/Unicity/blob/master/unicity.png)

# Unicity
Extensions for auto-incrementing numbers in non-database settings.

## Gettings started

```c#
//The IAutoIncrementedId<T> interface forces your implementing types to have a T Id with a get accessor
public record YourClass : IAutoIncrementedId<int>
{
	public int Id { get; init; }

	...
}

public void SomeMethod()
{
	var yourObject = new YourClass 
	{ 
		//Will get the next available number from the _items collection (items in collection must implement IAutoIncrementedId<T>)
		Id = _items.GetNextAvailableId();
		... 
	};

	//Alternatively, if you have no control over the type or don't want to implement the IAutoIncremented<T> interface
	//you can use the GetNextAvailableNumberOrDefault which uses a lambda
	//please note that this method currently only supports Int32
	var yourOtherObject = new YourClassWithoutId
	{
		Number = _otherItems.GetNextAvailableNumberOrDefault(x => x.Number)
	}

	//If you're weird you can also specify a new "default" value if your _otherItems collection is empty 
	//(5 in this case would be the first number)
	var yourOtherWeirderObject = new YourClassWithoutId
	{
		Number = _otherItems.GetNextAvailableNumberOrDefault(x => x.Number, 5)
	}
}
```