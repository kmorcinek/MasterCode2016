using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace Containers
{
    public class ContainersTests
    {
        private readonly Containers _sut;

        public ContainersTests()
        {
            _sut = new Containers();
        }

        [Theory]
        [InlineData(7, new[] { 7 }, 1)]
        public void Test1(int fuel, int[] containers, int result)
        {
            var wrappedContainer = new List<int>(containers);

            var countCombinations = _sut.countCombinations(fuel, wrappedContainer);

            Assert.Equal(result, countCombinations);
        }
    }
}