using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using Xunit.Extensions;

namespace _03_xUnit
{
    /// <summary>
    ///     xUnit.NET homepage: http://xunit.codeplex.com/
    /// 
    ///     Useful blog entries:
    ///     http://blog.ploeh.dk/tags.html#xUnit.net-ref
    ///     http://www.tomdupont.net/2012/04/xunit-theory-data-driven-unit-test.html
    /// </summary>
    public class XunitDemoTests
    {
        [Fact]
        public void SimpleTest()
        {
            Assert.Equal(4, 2 + 2);
        }

        // ======================================================================
        // Injecting test data
        // ======================================================================

        [Theory]
        [InlineData(2, 3, 5)]
        [InlineData(3, 3, 6)]
        [InlineData(3, 0, 3)]
        public void UsingInlineData(int a, int b, int expected)
        {
            var sut = new MyClass();
            int actual = sut.Add(a, b);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof (TestDataProvider))]
        public void UsingPropertyData(int a, int b, int expected)
        {
            var sut = new MyClass();
            int actual = sut.Add(a, b);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof (MyDTODataProvider))]
        public void UsingTestCaseSourceWithTastCaseDataWithObjects(MyDTO myDTO, decimal expected)
        {
            var sutSystem = new MySystem();
            Assert.Equal(expected, sutSystem.Calculate(myDTO));
        }
    }


    public class MyClass
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }


    public class TestDataProvider : IEnumerable<object[]>
    {
        private readonly List<object[]> data = new List<object[]>
            {
                new object[] {1, 2, 3},
                new object[] {2, 3, 5}
            };

        public IEnumerator<object[]> GetEnumerator()
        {
            return data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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

    public class MyDTODataProvider : IEnumerable<object[]>
    {
        private readonly List<object[]> data = new List<object[]>
            {
                new object[] {new MyDTO {Price = 23}, new Decimal(33)},
                new object[] {new MyDTO {Price = 0}, new Decimal(10)}
            };

        public IEnumerator<object[]> GetEnumerator()
        {
            return data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}