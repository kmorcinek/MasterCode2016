using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightBulbs
{
    public abstract class ILightBulbs
    {
        /// <summary>
        /// Put implementation creating your derived class instance in derived class source code file.
        /// Use the 'new' keyword to overwrite default behaviour.
        /// </summary>
        /// <returns></returns>
        public static ILightBulbs GetInstance()
        {
            /* TO DO - create your object */
            // lightBulbsInstance = new ...;
            // return lightBulbsInstance;
            throw new NotImplementedException();
        }

        public readonly static int boardSize = 78;


        public abstract int CountLightsOn(bool [,] lightsBoard, int s);

        
        protected static ILightBulbs lightBulbsInstance;
    }
}
