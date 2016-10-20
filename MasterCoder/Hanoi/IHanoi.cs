using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanoi
{
    public abstract class IHanoi
    {
        public abstract int getNumberOfDisks();
        public abstract bool moveDisk(uint fromRod, uint toRod);
        public abstract int checkTopDisk(uint rod);
    }

    public class Hanoi : IHanoi
    {
        const int NumberOfPegs = 4;

        readonly int _getNumberOfDisks;
        List<Stack<int>> _pegs;

        public IEnumerable<int> EvenRod()
        {
            return _pegs[2];
        }

        public IEnumerable<int> OddRod()
        {
            return _pegs[3];
        }

        public Hanoi(int getNumberOfDisks)
        {
            _getNumberOfDisks = getNumberOfDisks;

            InitPegs(_getNumberOfDisks);
        }

        void InitPegs(int disks)
        {
            _pegs = new List<Stack<int>>(NumberOfPegs);

            for (int i = 0; i < NumberOfPegs; i++)
            {
                _pegs.Add(new Stack<int>());
            }

            for (int i = 0; i < disks; ++i)
            {
                _pegs[0].Push(disks - i - 1);
            }
        }

        public override int getNumberOfDisks()
        {
            return _getNumberOfDisks;
        }

        public override bool moveDisk(uint fromRod, uint toRod)
        {
            //Debug.WriteLine("\t{0} -> {1}", fromRod, toRod);

            var pop = _pegs[(int)fromRod].Pop();
            _pegs[(int)toRod].Push(pop);

            return true;
        }

        public override int checkTopDisk(uint rod)
        {
            if (_pegs[(int)rod].Count == 0)
            {
                return -1;
            }

            return _pegs[(int)rod].Peek();
        }
    }
}
