# IIGen
[![NuGet: YT.IIGen](https://img.shields.io/nuget/v/YT.IIGen)](https://www.nuget.org/packages/YT.IIGen)
[![NuGet: Downloads](https://img.shields.io/nuget/dt/YT.IIGen)](https://www.nuget.org/packages/YT.IIGen)

Interface and implementation generator for .NET types.

This library is useful in case you want to have possibilities to mock static classes, classes without interfaces and
structs in your tests.

## Requirements
* .NET 6 and higher

## How to use
#### 1. Install NuGet package
Use any approach you prefer to install the NuGet package `YT.IIGen`.
#### 2. Declare an interface
Declare your interface for a type you want to make mockable, use `IIForAttribute` to specify the type to wrap
and the name of the wrapper class to be generated.
```csharp
using YT.IIGen.Attributes;

[IIFor(typeof(DateTime), "DateTimeWrapper")]
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

## Known limitations
* The types without default constructor are not supported yet.
* Generic methods are not supported yet.
