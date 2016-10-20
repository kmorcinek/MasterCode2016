using Xunit;

namespace LightBulbs
{
    public class LightBulbsTests
    {
        private readonly LightBulbs _sut;

        public LightBulbsTests()
        {
            _sut = new LightBulbs();
        }

        [Fact]
        public void Test()
        {
            bool[,] table = new bool[6, 6];
            table[0, 0] = true;

            var count = _sut.CountLightsOn(table, 1);

            Assert.Equal(0, count);
        }
    }
}