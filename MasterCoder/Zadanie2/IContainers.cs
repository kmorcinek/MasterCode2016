using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Containers
{
    public abstract class IContainers
    {
        /// <summary>
        /// Put implementation creating your derived class instance in derived class source code file.
        /// Use the 'new' keyword to overwrite default behaviour.
        /// </summary>
        /// <returns></returns>
        public static IContainers GetInstance()
        {
            return m_containersInstance;
        }

        /// <summary>
        /// Count number of minimum used containers.
        /// </summary>
        /// <param name="fuelVolume">Amount of fuel to be transported</param>
        /// <param name="containers">List of available containers (their volumes)</param>
        /// <returns>Minimum amount of containers required to transport given volume of fuel.</returns>
        public abstract int countCombinations(int fuelVolume, List<int> containers);

        protected static IContainers m_containersInstance;
    }
}
