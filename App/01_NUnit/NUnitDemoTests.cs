using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace _01_NUnit
{
    /// <summary>
    ///     NUnit documentation: http://www.nunit.org/
    /// </summary>
    [TestFixture]
    public class NUnitDemoTests
    {
        
        [Test]
        public void FirstTest()
        {
            // Arrange
            var systemUnderTest = new MyClass(); // sut: System Under Test

            // Act
            int actual = systemUnderTest.Add(3, 2);

            // Assert
            const int expected = 5;
            Assert.AreEqual(expected, actual);
        }

        // ======================================================================
        // 1. SetUp, TearDown, TestFixtureSetUp, TestFixtureTearDown
        // ======================================================================
        
        private MyClass sut;

        [SetUp]
        public void SetUp()
        {
            sut = new MyClass();
        }

        // ======================================================================
        // 2. Injecting test data
        // ======================================================================

        [Test]
        [TestCase(3, 2, 5)]
        [TestCase(3, 3, 6)]
        [TestCase(3, 0, 3)]
        public void UsingTestCases(int a, int b, int expected)
        {
            int actual = sut.Add(a, b);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCaseSource(typeof (TestDataProvider))]
        public void UsingTestCaseSource(int number)
        {
            Assert.AreEqual(0, number % 2);
        }

        [Test]
        [TestCaseSource(typeof(AnotherDataProvider))]
        public void UsingTestCaseSourceWithTestCaseData(int a, int b, int expected)
        {
            int actual = sut.Add(a, b);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCaseSource(typeof(MyDTODataProvider))]
        public decimal UsingTestCaseSourceWithTestCaseDataWithObjects(MyDTO myDTO)
        {
            var sutSystem = new MySystem();
            return sutSystem.Calculate(myDTO);
        }
    }

    public partial class MyClass
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }


    public class TestDataProvider : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            return new List<int> {2, 4, 6}.GetEnumerator();
        }
    }

    public class AnotherDataProvider : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            return new List<TestCaseData>
                {
                    new TestCaseData(1, 2, 3),
                    new TestCaseData(2, 2, 4)
                }.GetEnumerator();
        }
    }

    public class MySystem
    {
        public decimal Calculate(MyDTO myDTO)
        {
            return myDTO.Price + 10;
        }
    }

    public class MyDTO
    {
        public decimal Price { get; set; }
    }

    public class MyDTODataProvider : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            return new List<TestCaseData>
                {
                    new TestCaseData(new MyDTO {Price = 23}).Returns(33),
                    new TestCaseData(new MyDTO {Price = 0}).Returns(10)
                }.GetEnumerator();
        }
    }
}