using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace StockExchangeProfit
{
    public class StockExchangeProfitTests
    {
        // Dla 2 elementów.

        [Fact]
        public void Test1()
        {
            Execute(new List<int>(new[] {5, 10}), new TaskResult
            {
                LeftIndex = 0,
                RightIndex = 1,
                Value = 5
            });
        }

        [Fact]
        public void TestBig()
        {
            var list = Enumerable.Range(1, 1000001).ToList();

            Execute(list, new TaskResult
            {
                LeftIndex = 0,
                RightIndex = 1000000,
                Value = 1000000
            });
        }

        public void Execute(List<int> list, TaskResult expected)
        {
            var sut = new StockExchangeProfit();

            var result = sut.FindMaxProfitRecursive(list);

            Assert.Equal(expected.Value, result.Value);
            Assert.Equal(expected.LeftIndex, result.LeftIndex);
            Assert.Equal(expected.RightIndex, result.RightIndex);
        }

        [Fact]
        public void Test()
        {
            var list = new List<int>(new[] {5, 10, 2, 1, 3, 4, 9, 9, 8, 8});

            var sut = new StockExchangeProfit();

            var result = sut.FindMaxProfitRecursive(list);

            Assert.Equal(8, result.Value);
            Assert.Equal(3, result.LeftIndex);
            Assert.Equal(6, result.RightIndex);
        }
    }
}