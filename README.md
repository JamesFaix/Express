# Express

Express is a C# library for generating extension methods to allow imperative language features to be used more fluently in a functional style.

The library itself only generates source code text.  Client applications can use T4, Roslyn, or other tools to use the generated code.

## Features

### `Set`

Express can generate `Set[PropertyName]` extension methods for instance properties with public setters.  These methods will assign a value to the property and then return the object containing the property.

### `SetItem`

Express can generate `SetItem` extension methods for indexers with public setters.  These methods will assign a value to the index and then return the object containing the indexer.

### `Do`

Express can generate `Do[MethodName]` extension methods for public instance methods returning `void`.  These methods will call the method and then return the object containing the method.


## Examples

### Setting properties

One common C# idiom that can be awkward when programming in a functional style is setting property values.

For example, say we need to find an address in a list of addresses, modify that address, and then make it do something.  

    var address = addressBook.GetAddresses()
		.Single(a => a.Street == "123 Pine St.");

	address.ZipCode = "56789";
    address.IsBusiness = false;

    address.SaveAndPublishUpdate();

`Set[PropertyName]` extension methods allow this example to be written as:

    addressBook.GetAddresses()
		.Single(a => a.Street == "123 Pine St.");
        .SetZipCode("56789")
        .SetIsBusiness(false)
        .SaveAndPublishUpdate();

This is a very simple example, so the added benefit of Express is not that great, but it does eliminate the need for 1 local variable which had to be referenced 4 times before.
___

### Setting indexers

Similar to property assignments, working with indexers in expression chains can be awkward.

    var contactLists = GetContactLists();
    foreach (var c in contactLists) {
        c["Customer Support"] = updatedCustomerSupportContactInfo; 
    }
    return contactLists.Take(10);

It is possible to refactor this using statement lambdas, like this:

    return GetContactLists()
        .Select(c => {
            c["Customer Support"] = updatedCustomerSupportContactInfo;
            return c;
         })
        .Take(10);

Or to create an extension method `DoEach` that will execute some action for each element in the sequence and return the input element.

    return GetContactLists()
        .DoEach(c => {
            c["Customer Support"] = updatedCustomerSupportContactInfo;
         })
        .Take(10);


`SetItem` extension methods allow this example to be written as:

    return GetContactLists()
        .Select(c => c.SetItem("Custom Support", updatedCustomerSupportContactInfo))
        .Take(10);

This allows all imperative syntax to be avoided (blocks, loops, assignment).

____


### Continuing after calling a `void` method

It is pretty common to call several methods on an object in series which all return `void`.  For example,

    var dog = GetDog();
    dog.Bark();
    dog.Sit();
    dog.RollOver();

`Do[MethodName]` extension methods allow the example above to be written as:

    GetDog()
        .DoBark()
        .DoSit()
        .RollOver();

Alternately, the last call could be changed to `DoRollOver()` to return the dog instance;

___

### Object and collection initializers

`Set[PropertyName]` methods can also be used in place of object initializer expressions.  For example,

    var dog = new Dog {
        Name = "Pavlov",
        Color = Colors.Brown,
        Height = 33
    };

could be written as:

    var dog = new Dog()
        .SetName("Pavlov")
        .SetColor(Colors.Brown)
        .SetHeight(33);

Likewise, a `DoAdd` method can also be used in place of collection initializers expressions. For example,

    var myList = new List<Person> {
        new Person("Ada"),
        new Person("Bill"),
        new Person("Cathy"),
        new Person("Dave")
    };

could be written as:

    var myList = new List<Person>()
        .DoAdd(new Person("Ada"))
        .DoAdd(new Person("Bill"))
        .DoAdd(new Person("Cathy"))
        .DoAdd(new Person("Dave"));

Replacing initializers with these extension methods does not change the appearance of the consuming code very much, but I would argue there are several benefits to doing this:

1. The role that initializer expressions fulfill is expressed using the standard syntax of the language.  This means less language features to learn for new developers.  

2. It emphasizes the order that operations are happening in.  The initializer syntaxes do not make it clear that each assignment or call to `Add` happens *after* the constructor, not during it.

3. The same syntax can be used to "initialize" immediately after a constructor (like initializer syntax) or after any method.  

4. Refactoring around the constructor is easier. If using initializers, refactoring to use a Factory instead of a constructor requires more work. Similarly, when debugging, in order to put a breakpoint between a constructor and a following initializer, the initializer must be rewritten as normal property assignments or `Add` method calls.  Using `Set` or `Do` methods simplifies these refactorings.
      
### Constructor refactoring

##### Refactoring to Factory method with object initializer

    var dog = new Dog {
        Name = "Pavlov",
        Color = Colors.Brown,
        Height = 33
    };

All 4 lines must be modified.

	var dog = DogFactory.Create();
    dog.Name = "Pavlov";
    dog.Color = Colors.Brown;
    dog.Height = 33;     

##### Refactoring to Factory method with `Set[PropertyName]`

    var dog = new Dog()
        .SetName("Pavlov")
        .SetColor(Colors.Brown)
        .SetHeight(33);

Only the first line must be modified.

    var dog = DogFactory.Create()
        .SetName("Pavlov")
        .SetColor(Colors.Brown)
        .SetHeight(33);

##### Inserting breakpoint with object initializer

    var dog = new Dog {
        Name = "Pavlov",
        Color = Colors.Brown,
        Height = 33
    };

All 4 lines must be modified.

	var dog = new Dog();
    dog.Name = "Pavlov"; //Set breakpoint here
    dog.Color = Colors.Brown;
    dog.Height = 33;     

##### Inserting breakpoint method with `Set[PropertyName]`

    var dog = new Dog()
        .SetName("Pavlov")
        .SetColor(Colors.Brown)
        .SetHeight(33);

Only the first 2 lines must be modified.

    var dog = new Dog();
    dog = dog.SetName("Pavlov") //Set breakpoint here
        .SetColor(Colors.Brown)
        .SetHeight(33);
