using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Party
{
    public abstract class IParty
    {
        /// <summary>
        /// Put implementation creating your derived class instance in derived class source code file.
        /// Use the 'new' keyword to overwrite default behaviour.
        /// </summary>
        /// <returns></returns>
        public static IParty GetInstance()
        {
            /* TO DO - create your object */
            // m_partyInstance = new ...;
            // return m_partyInstance;
            throw new NotImplementedException();
        }

		/// <summary>
        /// Method to set up guests at the party
        /// </summary>
        /// <param name="guestList">Guests list</param>        
        /// <param name="noOfGuests">Number of guests</param>        
        /// <param name="maxDifference">Max age difference around one table</param>        
        /// <param name="noOfTables">Number of available tables</param>
        /// <param name="chairsPerTable">Number of chairs per table</param>        
        /// <param name="answer">Answer</param>   
		/// <returns></returns>
        public abstract void placeGuests(List<int> guestList,
										  uint noOfGuests,
										  uint maxDifference,
										  uint noOfTables,
										  uint chairsPerTable,
										  List<int> answer);

        /// <summary>
        /// Instance of object
        /// </summary>
        protected static IParty m_partyInstance;
    }
}
