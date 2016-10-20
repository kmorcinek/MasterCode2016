using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MasterCoder.Tools;
using Xunit;

namespace FrequencyDistributor
{
    public class FrequencyDistributorTests
    {
        readonly FrequencyDistributor _sut;

        public FrequencyDistributorTests()
        {
            _sut = new FrequencyDistributor();
        }

        [Fact]
        public void WrongMinDataOn2v2()
        {
            List<int> distribute = _sut.distribute(2, 2, new List<int>(new[] { 1, 4 }), 4);

            Assert.Equal(0, distribute.Count);
        }

        [Fact]
        public void Map_1v1_can_handle_even_oneFrequence()
        {
            List<int> distribute = _sut.distribute(1, 1, new List<int>(new[] { 1 }), 5);

            Assert.Equal(1, distribute.Count);
        }

        [Theory]
        [InlineData(1, 5)]
        [InlineData(5, 1)]
        public void Map_1vx_can_NOT_handle_even_twoFrequencies_less_than_min(uint dimA, uint dimB)
        {
            List<int> distribute = _sut.distribute(dimA, dimB, TwoCorrectFrequencies.Frequencies, TwoCorrectFrequencies.MinDiff);

            Assert.Equal((int)(dimA * dimB), distribute.Count);
        }

        [Theory]
        [InlineData(1, 5)]
        [InlineData(5, 1)]
        public void Map_1vx_can_handle_even_twoFrequencies_bigger_than_min(uint dimA, uint dimB)
        {
            List<int> distribute = _sut.distribute(dimA, dimB, TwoCorrectFrequencies.Frequencies, 1111);

            Assert.Equal(0, distribute.Count);
        }

        TwoFrequencies TwoCorrectFrequencies
        {
            get
            {
                return new TwoFrequencies()
                {
                    Frequencies = new List<int>(new[] { 1, 4 }),
                    MinDiff = 3
                };
            }
        }

        TwoFrequencies FourCorrectFrequencies
        {
            get
            {
                return new TwoFrequencies()
                {
                    Frequencies = new List<int>(new[] { 1, 4, 7, 10 }),
                    MinDiff = 3
                };
            }
        }

        class TwoFrequencies
        {
            public List<int> Frequencies;
            public uint MinDiff;
        }

        [Theory]
        [InlineData(new[] { 1, 4, 7, 10 })]
        [InlineData(new[] { 1, 4, 7, 9 })]
        public void Frequencies(int[] collection)
        {
            var frequenicesQuadrat = _sut.GetFrequencies(new List<int>(collection), 3);
        }

        [Fact]
        public void TestPositive2v2()
        {
            List<int> distribute = _sut.distribute(2, 2, FourCorrectFrequencies.Frequencies, FourCorrectFrequencies.MinDiff);

            Asserts.AssertArray(new[] { 1, 4, 7, 10 }.ToList(), distribute);
        }

        [Fact]
        public void TestPositive4v2()
        {
            List<int> distribute = _sut.distribute(4, 2, FourCorrectFrequencies.Frequencies, FourCorrectFrequencies.MinDiff);

            Asserts.AssertArray(new[] { 1, 4, 1, 4, 7, 10, 7, 10 }.ToList(), distribute);
        }

        [Fact]
        public void TestPositive2v4()
        {
            List<int> distribute = _sut.distribute(2, 4, FourCorrectFrequencies.Frequencies, FourCorrectFrequencies.MinDiff);

            Asserts.AssertArray(new[] { 1, 4, 7, 10, 1, 4, 7, 10 }.ToList(), distribute);
        }

        [Fact]
        public void TestOddDimA()
        {
            List<int> distribute = _sut.distribute(3, 2, new List<int>(new[] { 1, 4, 7, 10 }), 3);
        }

        [Fact]
        public void On3on3()
        {
            List<int> distribute = _sut.distribute(3, 3, new List<int>(new[] { 1, 4 }), 3);
        }
    }
}