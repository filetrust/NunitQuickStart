# NUnitQuickStart

This is a quick start to utilising the NUnit testing framework and demonstrates how we can unit test .Net C# code. Utilising .Net just requires adding a NUnit test project to your solution.

The NUnitQuickStart solution has two projects:

- A project to illustrate code under test
- An NUnit test project to illustrate unit testing with NUnit. 

### Code Under Test Project

Consider a project that has the following code that we would like to unit test:

- An interface called `IMyInterface`.

```
using System;
using System.Collections.Generic;
using System.Text;

namespace SUT.Interfaces
{
    public interface IMyInterface
    {
        string f();
    }
}
```

- A class called `ClassA` derives from `IMyInterface` and has a method `Class::f()`  that returns a string

```
using SUT.Interfaces;

namespace SUT
{
    public class ClassA : IMyInterface
    {
        public string f()
        {
            return "I am a real class A";
        }
    }
}
```

- A class called `ClassB` that has a method `ClassB::g()` that returns a string using an instance of IMyInterface passed as a parameter.

```
using SUT.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SUT
{
    public class ClassB
    {
        public string g(IMyInterface myInterface)
        {
            return myInterface.f();
        }
    }
}
```

### The Unit Test Project

 A standard NUnit test project, a standard console executable, is created and then:

- Project properties are made to reference code under test.

- A ClassB.Tests.cs is created to test class `ClassB` 

The ClassB.Tests.cs has two test cases using a modern [AAA](https://www.thephilocoder.com/unit-testing-aaa-pattern/) (Arrange, Act and Assert testing pattern layout).

**Test Case 1:**

```
            [Test]
            public void ClassB_Is_Using_Real_ClassA()
            {
                // Arrange
                var classA = new ClassA();
                var classB = new ClassB();

                // Act
                var result = classB.g(classA);

                // Assert
                Assert.That(result.Equals("I am a real class A"));
            }
```

- In the above test case, we **arrange** two classes
  - Our interest lies in `classB` which is the code that is under test
  - `ClassA` is used to help facilitate the testing of `classB`
- We **act** by calling `class.B.g` which relies on on an instance of `ClassA` being passed to it
- We **assert** that the string is returned by `classA`.

At this point, we can run the project test executable and we will see all our tests being run. 

The tests are visible in the Visual Studio Test Explorer.

## Moq Mocking Framework

With a codebase that uses an C#, a good mocking framework should be used with the unit testing framework.  The [Moq](https://github.com/moq/moq4) mocking framework provides us with an ability to remove the need to worry about creating classes, such as `ClassA`, and have better control over how they interact with the class under test, that is `ClassB`. 

**Test Case 2:**

```
            [Test]
            public void ClassB_Is_Using_Mock_ClassA()
            {
                // Arrange
                var mockClassA = new Mock<IMyInterface>();
                var classB = new ClassB();

                mockClassA.Setup(x => x.f()).Returns("I am a mock of class A");

                // Act
                var result = classB.g(mockClassA.Object);

                // Assert
                Assert.That(result.Equals("I am a mock of class A"));
            }
```

In our test 2, we now use Moq to arrange a mock of `ClassA`:

- Mocking a class is only possible if we use an interface: `ClassA` is an instance of IMyInterface
- The mockClassA.Setup(...) in the Arrange section is showing that our mock of `ClassA` must return this particular string when `ClassA::f()`  is invoked.

