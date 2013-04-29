using System.Collections;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoFakeItEasy;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.AutoNSubstitute;

namespace _04_DependencyMockingLibraries
{
    /* NO MOCK 01 =========================================================

    /// <summary>
    /// Simple dependency
    /// </summary>
    [TestFixture]
    public class MockingFrameworkDemoTests
    {
        [Test]
        public void FirstTryWithoutAnyFramework()
        {
            var anotherDependency = new AnotherDependency();
            var myDependency = new MyDependency(anotherDependency);
            var mySystem = new MySystem(myDependency);

            string actual = mySystem.DoSomething("foo");
            Assert.AreEqual("REALMAGICfoo", actual);
        }
    }
    
    * =====================================================================*/


    [TestFixture]
    public class MockingFrameworkDemoTests
    {
        [Test]
        public void WithAutoFixtureAndAutoMoq()
        {
            IFixture fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mySystem = fixture.Create<MySystem>();

            string actual = mySystem.DoSomething("foo");
            Assert.That(actual, Is.StringStarting("REALMAGIC"));
        }

        [Test]
        public void WithAutoFixtureAndFakeItEasy()
        {
            IFixture fixture = new Fixture().Customize(new AutoFakeItEasyCustomization());
            var mySystem = fixture.Create<MySystem>();

            string actual = mySystem.DoSomething("foo");
            Assert.That(actual, Is.StringStarting("REALMAGIC"));
        }

        [Test]
        public void WithAutoFixtureAndNSubstitute()
        {
            IFixture fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            var mySystem = fixture.Create<MySystem>();

            string actual = mySystem.DoSomething("foo");
            Assert.That(actual, Is.StringStarting("REALMAGIC"));
        }

        [Test, TestCaseSource(typeof (MyDataProvider))]
        public void WithAutoFixtureAndTestCaseSource(MySystem sut)
        {
            string actual = sut.DoSomething("foo");
            Assert.That(actual, Is.StringStarting("REALMAGIC"));
        }

        [Test, Ignore]
        public void WithMockedDependenciesUsingAutoMoq()
        {
            var mockedDependency = new Mock<MyDependency>();
            var mySystem = new MySystem(mockedDependency.Object);

            Assert.IsInstanceOf<MySystem>(mySystem);
        }
    }

    public class MySystem
    {
        private readonly MyDependency myDependency;

        public MySystem(MyDependency dependency)
        {
            myDependency = dependency;
        }

        public string DoSomething(string s)
        {
            return myDependency.DoMagic(s);
        }
    }

    public class MyDependency
    {
        private readonly AnotherDependency dependency;

        public MyDependency(AnotherDependency anotherDependency)
        {
            dependency = anotherDependency;
        }

        public string DoMagic(string s)
        {
            return dependency.DoRealMagic("MAGIC" + s);
        }
    }

    public class AnotherDependency
    {
        public string DoRealMagic(string s)
        {
            return "REAL" + s;
        }
    }

    public class MyDataProvider : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            IFixture fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            var mySystem = fixture.Create<MySystem>();

            return new List<TestCaseData>
                {
                    new TestCaseData(fixture, mySystem)
                }
                .GetEnumerator();
        }
    }
}