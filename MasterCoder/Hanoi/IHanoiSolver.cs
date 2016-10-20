using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanoi
{
    public abstract class IHanoiSolver
    {
		
		protected static IHanoiSolver m_hanoiSolverInstance;


        /// <summary>
        /// Put implementation creating your derived class instance in derived class source code file.
        /// Use the 'new' keyword to overwrite default behaviour.
        /// </summary>
        /// <returns></returns>
        public static IHanoiSolver GetInstance()
        {
            /* TO DO - create your object */
            // m_hanoiSolverInstance = new ...;
            // return m_hanoiSolverInstance;
            throw new NotImplementedException();
        }

        public abstract void solveHanoi(IHanoi hanoi);


	}
}
