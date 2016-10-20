//using Xunit;

//    public class FactorialTrailingZerosTests
//    {
//        // Test na 2^32 bo SO
//        readonly FactorialTrailingZeros _sut;

//        public string Name { get; set; }

//        public FactorialTrailingZerosTests()
//        {
//            _sut = new FactorialTrailingZeros();
//        }

//        [Theory]
//        [InlineData(11, 3, 4)]
//        [InlineData(13, 7, 1)]
//        [InlineData(15, 10, 3)]
//        [InlineData(30, 16, 6)]
//        public void Test(int number, int @base, int expected)
//        {
//            var calculateCount = _sut.CalculateCount(number, @base);

//            Assert.Equal(expected, calculateCount);
//        }

//        [Theory]
//        [InlineData(16)]
//        [InlineData(2)]
//        public void Test2(int @base)
//        {
//            // 2^31
//            var big = 2147483648;
//            _sut.CalculateCount((int)big, @base);
//        }

//        [Theory]
//        [InlineData(1)]
//        [InlineData(0)]
//        public void TestSmall(int number)
//        {
//            var result = _sut.CalculateFactorial(number);

//            Assert.Equal(1, result);
//        }
//    }