using FakeItEasy;
using Moq;
using NSubstitute;
using NUnit.Framework;

namespace _02_MockingLibraries
{
    /// <summary>
    ///     Comparison of popular mocking frameworks.
    /// 
    ///     All frameworks support mocking of:
    ///     - methods
    ///     - properties
    ///     - events.
    /// 
    ///     All frameworks require an interface (or an abstract class with virtual members) for mocking.
    /// 
    ///     These tests show that the differences are mainly in syntax.
    /// 
    ///     Refer to project pages for details:
    /// 
    ///         Moq: https://code.google.com/p/moq/
    ///         NSubstitute: http://nsubstitute.github.io/
    ///         FakeItEasy:https://github.com/FakeItEasy/FakeItEasy
    /// </summary>
    [TestFixture]
    public class MockingFrameworkDemoTests
    {
        /// <summary>
        ///     FakeItEasy: Mocking a property
        /// </summary>
        [Test]
        public void MyClass_Do_Returns_CorrectProperty_Using_FakeItEasy()
        {
            var mockedDependency = A.Fake<IMyDependency>();
            mockedDependency.Name = "MockedName";
            Assert.AreEqual("MockedName", mockedDependency.Name);
        }

        /// <summary>
        ///     Moq: Mocking a property
        /// </summary>
        [Test]
        public void MyClass_Do_Returns_CorrectProperty_Using_Moq()
        {
            var mockedDependency = new Mock<IMyDependency>();
            mockedDependency.Setup(x => x.Name).Returns("MockedName");
            Assert.AreEqual("MockedName", mockedDependency.Object.Name);
        }

        /// <summary>
        ///     NSubstitute: Mocking a property
        /// </summary>
        [Test]
        public void MyClass_Do_Returns_CorrectProperty_Using_NSubstitute()
        {
            var mockedDependency = Substitute.For<IMyDependency>();
            mockedDependency.Name = "MockedName";
            Assert.AreEqual("MockedName", mockedDependency.Name);
        }

        /// <summary>
        ///     FakeItEasy: Mocking a method
        /// </summary>
        [Test]
        public void MyClass_Do_Returns_CorrectValue_Using_FakeItEasy()
        {
            var mockedDependency = A.Fake<IMyDependency>();
            A.CallTo(() => mockedDependency.DoMagic(A<string>.Ignored)).Returns("FOO");

            var sut = new MyClass(mockedDependency);
            string actual = sut.Do("test");
            Assert.AreEqual("DoFOO", actual);
        }

        /// <summary>
        ///     MOQ: Mocking a method
        /// </summary>
        [Test]
        public void MyClass_Do_Returns_CorrectValue_Using_Moq()
        {
            var mockedDependency = new Mock<IMyDependency>();
            mockedDependency.Setup(x => x.DoMagic(It.IsAny<string>())).Returns("FOO");

            var sut = new MyClass(mockedDependency.Object);
            string actual = sut.Do("test");
            Assert.AreEqual("DoFOO", actual);
        }

        /// <summary>
        ///     NSubstitute: Mocking a method
        /// </summary>
        [Test]
        public void MyClass_Do_Returns_CorrectValue_Using_NSubstitute()
        {
            var mockedDependency = Substitute.For<IMyDependency>();
            mockedDependency.DoMagic(Arg.Any<string>()).Returns("FOO");

            var sut = new MyClass(mockedDependency);
            string actual = sut.Do("test");
            Assert.AreEqual("DoFOO", actual);
        }
    }


    public class MyClass
    {
        private readonly IMyDependency myDependency;

        public MyClass(IMyDependency myDependency)
        {
            this.myDependency = myDependency;
        }

        public string Do(string s)
        {
            return "Do" + myDependency.DoMagic(s);
        }
    }


    public interface IMyDependency
    {
        string Name { get; set; }
        string DoMagic(string s);
    }

    public class MyDependency : IMyDependency
    {
        private readonly AnotherDependency anotherDependency;

        public MyDependency(AnotherDependency anotherDependency)
        {
            this.anotherDependency = anotherDependency;
        }

        public string DoMagic(string s)
        {
            return anotherDependency.SomeCheckIsValid() ? "Magic" + s : s;
        }

        public string Name { get; set; }
    }

    public class AnotherDependency
    {
        public bool SomeCheckIsValid()
        {
            return true;
        }
    }
}