using System.Collections.Generic;
using Xunit;

namespace MasterCoder.Tools
{
    public class Asserts
    {
        public static void AssertArray<T>(List<T> expected, List<T> result)
        {
            Assert.Equal(expected.Count, result.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i], result[i]);
            }
        }
    }
}