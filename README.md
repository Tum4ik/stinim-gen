<h1 align="center">
  <img src="logo.png" alt="StinimGen" style="width:128px;" />
  <br/>
  StinimGen
</h1>
<h1 align="center">

  [![NuGet: Tum4ik.StinimGen](https://img.shields.io/nuget/v/Tum4ik.StinimGen)](https://www.nuget.org/packages/Tum4ik.StinimGen)
  [![NuGet: Downloads](https://img.shields.io/nuget/dt/Tum4ik.StinimGen)](https://www.nuget.org/packages/Tum4ik.StinimGen)
</h1>

Interface and implementation generator for static members.

This library is useful in case you want to have possibilities to mock static classes or static members in your tests.

## Requirements
* .NET 6 and higher

## Philosophy
1. `Stinim` should be associated with the words: **st**atic, **in**terface, **im**plementation, `Gen` - **gen**erator.
2. Static (also const) **fields** are converted to the **properties**.
3. Static **events**, **properties** and **methods** are converted to the **events**, **properties** and **methods** respectively.
4. All instance (non-static) members are ignored.

## How to use
#### 1. Install NuGet package
Use any approach you prefer to install the NuGet package `Tum4ik.StinimGen`.
#### 2. Declare an interface
Declare your interface for a type you want to make mockable, use `IIForAttribute` to specify the type to wrap
and the name of the wrapper class to be generated.
```csharp
using Tum4ik.StinimGen.Attributes;

[IIFor(typeof(DateTime), WrapperClassName = "DateTimeWrapper")]
internal partial interface IDateTime
{
}
```
#### 3. Register
Register the declared interface and the generated wrapper class in the DI container.
```csharp
container.Register<IDateTime, DateTimeWrapper>();
```
#### 4. Inject and use
Inject and use your wrapper in any place you need.
```csharp
internal class MyService
{
  private readonly IDateTime _dateTime;

  public MyService(IDateTime dateTime)
  {
    _dateTime = dateTime;
  }


  public DateTime GetCurrentDateTime()
  {
    return _dateTime.Now;
  }
}
```
#### 5. Mock and test
Now you are able to mock the functionality as usual.
```csharp
using Moq;
using Xunit;

public class MyServiceTests
{
  private readonly Mock<IDateTime> _dateTimeMock = new()
  private readonly MyService _testeeService;

  public MyServiceTests()
  {
    _testeeService = new(_dateTimeMock.Object);
  }


  [Fact]
  internal void GetCurrentDateTimeTest()
  {
    var now = DateTime.Now;
    _dateTimeMock.Setup(dt => dt.Now).Returns(now);
    Assert.Equal(now, _testeeService.GetCurrentDateTime());
  }
}
```

## API
| Property | Default | Description |
|-|-|-|
| sourceType (constructor required) | - | The type to generate interface members and an implementation wrapper for |
| WrapperClassName (required) | - | The implementation wrapper class name to generate |
| IsPublic | false | Controls the generated implementation wrapper accessibility: `true` emits `public` access modifier, `false` - `internal` |
| IsSealed | true | Controls the ability to inherit generated implementation wrapper: `true` emits `sealed` modifier, `false` - nothing |
