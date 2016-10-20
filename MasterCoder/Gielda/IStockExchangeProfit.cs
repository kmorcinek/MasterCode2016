using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchangeProfit
{
    public class TaskResult  
    {
        public int LeftIndex { get; set; }
        public int RightIndex { get; set; }
        public int Value { get; set; }
    }


    public abstract class IStockExchangeProfit
    {
        /// <summary>
        /// Put implementation creating your derived class instance in derived class source code file.
        /// Use the 'new' keyword to overwrite default behaviour.
        /// </summary>
        /// <returns></returns>
        public static IStockExchangeProfit GetInstance()
        {
            /* TO DO - create your object */
            // stockInstance = new ...;
            // return stockInstance;
            throw new NotImplementedException();
        }

        public abstract TaskResult FindMaxProfitRecursive(List<int> values);


        protected static IStockExchangeProfit stockInstance;
    }
}
