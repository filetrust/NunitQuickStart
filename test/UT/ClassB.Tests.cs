using Moq;
using NUnit.Framework;
using SUT;
using SUT.Interfaces;

namespace UT
{
    [TestFixture]
    public class ClassBTests
    {
        [TestFixture]
        public class ClassBMethodTests : ClassBTests
        {
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

        }
    }
}
