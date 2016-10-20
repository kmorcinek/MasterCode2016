using System;
using System.Collections.Generic;
using Xunit;

namespace Hanoi
{
    public class HanoiSolverTests
    {
        HanoiSolver _sut = new HanoiSolver();

        [Theory]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(0)]
        [InlineData(16)]
        public void Test2(int disksNumber)
        {
            var hanoi = new Hanoi(disksNumber);
            _sut.solveHanoi(hanoi);

            AssertAreEven(hanoi.EvenRod());
            AssertAreOdd(hanoi.OddRod());
        }

        void AssertAreEven(IEnumerable<int> evenRod)
        {
            foreach (var item in evenRod)
            {
                Assert.True(item % 2 == 0);
            }
        }

        void AssertAreOdd(IEnumerable<int> evenRod)
        {
            foreach (var item in evenRod)
            {
                Assert.False(item % 2 == 0);
            }
        }

        [Fact]
        public void Test()
        {
        }
    }
}