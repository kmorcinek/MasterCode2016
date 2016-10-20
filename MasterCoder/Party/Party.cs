using System;
using System.Collections.Generic;
using System.Linq;

namespace Party
{
    public class Party : IParty
    {
        public new static IParty GetInstance()
        {
            m_partyInstance = new Party();
            return m_partyInstance;
        }

        public override void placeGuests(List<int> guestList, uint noOfGuests, uint maxDifference, uint noOfTables,
            uint chairsPerTable, List<int> answer)
        {
            placeGuestsInternal(guestList, (int)noOfGuests, (int)maxDifference, (int)noOfTables, (int)chairsPerTable, answer);
        }

        void placeGuestsInternal(List<int> guestList, int noOfGuests, int maxDifference, int noOfTables, int chairsPerTable, List<int> answer)
        {
            if (guestList.Count != noOfGuests)
            {
                throw new ArgumentException();
            }

            IEnumerable<int> enumerable = Enumerable.Range(0, noOfTables);
            var combinationsWithRepetition = CombinationsWithRepetition(enumerable.ToArray(), noOfGuests);

            foreach (List<int> indexes in combinationsWithRepetition)
            {
                List<List<int>> tables = new List<List<int>>(noOfTables);
                for (int i = 0; i < noOfTables; i++)
                {
                    tables.Add(new List<int>());
                }

                for (int i = 0; i < indexes.Count; i++)
                {
                    tables[indexes[i]].Add(guestList[i]);
                }

                bool isOK = CheckTables(tables, maxDifference, chairsPerTable);
                if (isOK)
                {
                    answer.Clear();
                    answer.AddRange(ChangeToTableResult(tables, chairsPerTable));
                    return;
                }
            }
        }

        List<int> ChangeToTableResult(List<List<int>> tables, int chairsPerTable)
        {
            var list = new List<int>(chairsPerTable * tables.Count);

            foreach (List<int> takenChairs in tables)
            {
                list.AddRange(takenChairs);
                var missingSeats = chairsPerTable - takenChairs.Count;
                list.AddRange(Enumerable.Repeat(0, missingSeats));
            }

            return list;
        }

        public bool CheckTables(List<List<int>> tables, int maxDifference, int chairsPerTable)
        {
            foreach (List<int> takenChairs in tables)
            {
                if (takenChairs.Count == 1)
                {
                    return false;
                }

                if (takenChairs.Count == 0)
                {
                    continue;
                }

                if (takenChairs.Count > chairsPerTable)
                {
                    return false;
                }

                if (diff(takenChairs) > maxDifference)
                {
                    return false;
                }
            }

            return true;
        }

        int diff(List<int> A)
        {
            int max = A.Max();
            int min = A.Min();

            return max - min;
        }

        public static IEnumerable<List<T>> CombinationsWithRepetition<T>(T[] input, int length)
        {
            if (length <= 0)
                yield return new List<T>();
            else
            {
                foreach (var i in input)
                    foreach (var c in CombinationsWithRepetition(input, length - 1))
                    {
                        c.Add(i);
                        yield return c;
                    }
            }
        }
    }
}