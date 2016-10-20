using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrequencyDistributor
{
	public abstract class IFrequencyDistributor
    {
        protected static IFrequencyDistributor m_frequencyDistributorInstance;

        /// <summary>
        /// Put implementation creating your derived class instance in derived class source code file.
        /// Use the 'new' keyword to overwrite default behaviour.
        /// </summary>
        /// <returns></returns>
        public static IFrequencyDistributor GetInstance()
        {
            /* TO DO - create your object */
            // m_frequencyDistributorInstance = new ...;
            // return m_frequencyDistributorInstance;
            throw new NotImplementedException();
        }

        public abstract List<int> distribute(uint dimA, uint dimB, List<int> frequencies, uint minDiff);

	}
	
}