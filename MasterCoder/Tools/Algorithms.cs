using System.Collections.Generic;
using System.Linq;

namespace MasterCoder.Tools
{
    public class Algorithms
    {
        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        public static List<List<T>> Change<T>(IEnumerable<IEnumerable<T>> list)
        {
            var result = new List<List<T>>();
            foreach (IEnumerable<T> innerList in list)
            {
                result.Add(new List<T>(innerList));
            }

            return result;
        }

        static IEnumerable<string> CombinationsWithRepetition(IEnumerable<char> input, int length)
        {
            if (length <= 0)
                yield return "";
            else
            {
                foreach (var i in input)
                    foreach (var c in CombinationsWithRepetition(input, length - 1))
                        yield return i + c;
            }
        }
    }
}