using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLConverter
{
    /// <summary>
    ///  Store all elements of query as list, without whitespaces 
    /// </summary>
    public class SqlQuery
    {
        /// <summary>
        ///  {"INSERT"
        ///  "INTO"
        ///  "table_name"
        ///  "("
        ///  "column1"
        ///  ","
        ///  "column2"
        ///  ","
        ///  "column3"
        ///  ")"
        ///  "VALUES"
        ///  "("
        ///  "value1"
        ///  ","
        ///  "value2"
        ///  ","
        ///  "value3"
        ///  ")"
        ///  ";"} 
        /// </summary>
        private List<string> m_components;
        public List<string> Components
        {
            get { return m_components; }
            set { m_components = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="comp"></param>
        public SqlQuery(List<string> comp)
        {
            m_components = comp;
        }
    }
}
