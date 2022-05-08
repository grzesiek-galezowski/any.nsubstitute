# Any.NSubstitute

Creates an instance of an interface using NSubstitute, but with method and property return values pre-canned with something different than defaults.

# How to use?

```csharp
var instance = Any.Substitute<IMyInstance>();

Assert.AreNotEqual(default, instance.GetInt1());
Assert.AreNotEqual(instance.GetInt1(), instance.GetInt2());
```