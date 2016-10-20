using Xunit;

namespace Decoder
{
    public class DecoderTests
    {
        private Decoder _decoder = new Decoder();

        [Theory]
        [InlineData("a", "abc")]
        [InlineData("ab", "abc")]
        [InlineData("aabc", "abcde")]
        [InlineData("sss", "0123456789abcdefghijklmnopqrs")]
        public void PositiveTestIdeaCases(string phrase, string alphabet)
        {
            PositiveTestIdea(phrase, alphabet);
        }

        void PositiveTestIdea(string phrase, string alphabet)
        {
            var value = Decoder.code(phrase, alphabet);
            var result = _decoder.decode(value, alphabet);

            Assert.Equal(ResultCode.SUCCESS, result.CodeResult);
            Assert.Equal(phrase, result.DecodedText);
        }

        [Fact]
        public void NegativeTest()
        {
            var result = _decoder.decode(20, "abcde");

            Assert.Equal(ResultCode.FAILURE, result.CodeResult);
            Assert.Equal("", result.DecodedText);
        }

//        [Fact]
//        public void Test3()
//        {
//            var code = Decoder.code("aa", "abcde");
//            var code2 = Decoder.code("e", "abcde");
//        }
    }
}