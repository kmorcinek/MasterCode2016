using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using Decoder;

namespace FrequencyDistributor
{
    public class FrequencyDistributor : IFrequencyDistributor
    {
        public new static IFrequencyDistributor GetInstance()
        {
            m_frequencyDistributorInstance = new FrequencyDistributor();
            return m_frequencyDistributorInstance;
        }

        public override List<int> distribute(uint dimA, uint dimB, List<int> frequencies, uint minDiff)
        {
            var list = new List<int>();
            if (frequencies == null || frequencies.Count == 0)
            {
                return list;
            }

            if (dimA == 1 && dimB == 1)
            {
                list.Add(frequencies[0]);
                return list;
            }

            if (dimA == 0 || dimB == 0)
            {
                return list;
            }

            frequencies.Sort();
            int min = frequencies.Min();
            int max = frequencies.Max();

            if ((dimA == 1 && dimB > 1) ||
                (dimA > 1 && dimB == 1))
            {
                if (max - min < minDiff)
                {
                    return list;
                }

                var cycler = new Cycler(min, max);
                for (int i = 0; i < dimA * dimB; i++)
                {
                    list.Add(cycler.GetNext());
                }

                return list;
            }

            // 2v2 starts

            List<List<int>> freqQuadrat = new List<List<int>>();
            freqQuadrat.Add(new List<int>());
            freqQuadrat.Add(new List<int>());
            freqQuadrat = GetFrequencies(frequencies, minDiff);
            if (freqQuadrat == null)
            {
                return list;
            }

            for (int b = 0; b < dimB; b++)
            {
                var bList = IsEven(b)
                    ? freqQuadrat[0]
                    : freqQuadrat[1];

                for (int a = 0; a < dimA; a++)
                {
                    var aIndex = a % 2;

                    list.Add(bList[aIndex]);
                }
            }

            return list;
        }

        public List<List<int>> GetFrequencies(List<int> frequencies, uint minDiff)
        {
            frequencies.Sort();

            List<List<int>> lists = new List<List<int>>
            {
                new List<int>(),
                new List<int>()
            };

            int first = frequencies[0];
            lists[0].Add(first);

            var second = frequencies.FirstOrDefault(x => x - minDiff >= first);
            if (second == 0)
            {
                return null;
            }
            lists[0].Add(second);

            var third = frequencies.FirstOrDefault(x => x - minDiff >= second);
            if (third == 0)
            {
                return null;
            }
            lists[1].Add(third);

            var fourth = frequencies.FirstOrDefault(x => x - minDiff >= third);
            if (fourth == 0)
            {
                return null;
            }
            lists[1].Add(fourth);

            return lists;
        }

        static bool IsEven(int input)
        {
            return input % 2 == 0;
        }

        class Cycler
        {
            readonly int _first;
            readonly int _second;
            bool _giveFirst = false;

            public Cycler(int first, int second)
            {
                _first = first;
                _second = second;
            }

            public int GetNext()
            {
                _giveFirst = !_giveFirst;

                if (_giveFirst)
                {
                    return _first;
                }

                return _second;
            }
        }
    }
}