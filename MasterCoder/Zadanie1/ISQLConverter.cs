using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLConverter
{
    public abstract class ISQLConverter
    {
        /// <summary>
        /// Put implementation creating ISQLConverter derived object
        /// </summary>
        /// <returns></returns>
        public static ISQLConverter GetInstance()
        {
            /* TO DO - create your object */
            // m_convInstance = new ...;

            return m_convInstance;
        }

        /// <summary>
        /// Method to convert a data input to sql query as output 
        /// </summary>
        /// <param name="tabName">Ex: USERS </param>        
        /// <param name="typesBuff">Ex: Types of columns ex: "STRING;STRING;STRING;INT;DATE"</param>        
        /// <param name="colNamBuff">Column name ex: "USER;DESCRIPTION;NOTE;NUMBER;LOGINDATE" </param>        
        /// <param name="dataBuff">Data ex: "MICHAEL;ADMIN;COMPANY;2051;2000-12-01 23:39
        ///                                  MICHAL;USER;COMPANY;2052;2000-12-01 00:00"</param>
        /// <returns></returns>
        public abstract List<SqlQuery> ConvertToSqlInsert(string tabName,
            string typesBuff,
            string colNamBuff,
            string dataBuff);

        /// <summary>
        /// Instance of object
        /// </summary>
        protected static ISQLConverter m_convInstance;
    }
}
