namespace Hanoi
{
    public class HanoiSolver : IHanoiSolver
    {
        const int MiddlePegWhenMoving = 2;
        const int EmptyRod = -1;
        IHanoi _hanoi;

        public new static IHanoiSolver GetInstance()
        {
            m_hanoiSolverInstance = new HanoiSolver();
            return m_hanoiSolverInstance;
        }

        public override void solveHanoi(IHanoi hanoi)
        {
            _hanoi = hanoi;

            var numberOfDisks = _hanoi.getNumberOfDisks();

            int disksToMove = numberOfDisks - 1;

            uint rodFrom = 0;
            uint rodTo = 1;

            while (true)
            {
                if (disksToMove <= 0)
                {
                    FirstRun(0);
                    FirstRun(1);

                    return;
                }

                Hanoi(disksToMove, rodFrom, MiddlePegWhenMoving, rodTo);
                FirstRun(rodFrom);

                disksToMove--;

                swap(ref rodTo, ref rodFrom);
            }
        }

        void swap(ref uint a, ref uint b)
        {
            uint c = a;
            a = b;
            b = c;
        }

        void FirstRun(uint startToCheckAndMove)
        {
            var topDisk = _hanoi.checkTopDisk(startToCheckAndMove);

            if (topDisk == EmptyRod)
            {
                return;
            }

            if (IsEven(topDisk))
            {
                // on 3rd
                _hanoi.moveDisk(startToCheckAndMove, 2);
            }
            else
            {
                // on 4th
                _hanoi.moveDisk(startToCheckAndMove, 3);
            }
        }

        bool IsEven(int number)
        {
            return number % 2 == 0;
        }

        // przekłada n krążków z A, korzystając z B, na C
        void Hanoi(int n, uint src, uint mid, uint dest)
        {
            if (n > 0)
            {
                Hanoi(n - 1, src, dest, mid);
                //                Console.WriteLine("\t{0} -> {1}", src, dest);
                _hanoi.moveDisk(src, dest);

                Hanoi(n - 1, mid, src, dest);
            }
        }
    }
}