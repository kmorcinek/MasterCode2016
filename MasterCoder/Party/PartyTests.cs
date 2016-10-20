using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Party
{
    public class PartyTests
    {
        private readonly Party _sut;

        public PartyTests()
        {
            _sut = new Party();
        }

        [Fact]
        public void Test()
        {
            //            uint a = 5;
            //            var b = (int) a;
            TestGeneric(new[] { 7, 3, 5, 1 }, 4, 2, 2, 2);
        }

        [Theory]
//        [InlineData(new [] { 22, 31, 25, 24, 30 }, 5, 2, 3, 4)]
        [InlineData(new [] { 22, 30 }, 2, 8, 1, 2)]
//        [InlineData(new [] { 22, 31, 25, 24, 30 }, 5, 8, 3, 4)]
        public void TestGeneric(int[] guestList, uint noOfGuests, uint maxDifference, uint noOfTables, uint chairsPerTable)
        {
            var answer = new List<int>();
            _sut.placeGuests(guestList.ToList(), noOfGuests, maxDifference, noOfTables, chairsPerTable, answer);

            Assert.True(answer.Count > 0);
        }

        [Fact]
        public void Test1()
        {
            var list = new List<List<int>>();
            var ints = new[] {new[] {22, 24}, new []{25}, new []{30,31}};
            foreach (int[] line in ints)
            {
                var lineList = new List<int>(line);
                list.Add(lineList);
            }

            var result = _sut.CheckTables(list, 2, 4);

            Assert.True(result);
        }

        //[Fact]
        //public void CheckChairDifference()
        //{
        //    var result = _party.CheckChair(new[] { 0, 2, 8 }, 7);

        //    Assert.True(result);
        //}

        //[Fact]
        //public void CheckChairNotAloneMan()
        //{
        //    var result = _party.CheckChair(new[] { 0, 2 }, 7);

        //    Assert.False(result);
        //}

        //[Fact]
        //public void CheckChairTooBigDiff()
        //{
        //    var result = _party.CheckChair(new[] { 12, 2 }, 7);

        //    Assert.False(result);
        //}
    }
}