// Nie wyslalem odpowiedzi

using System.Collections.Generic;

namespace Containers
{
    public class Containers : IContainers
    {
        /// <summary>
        /// Put implementation creating your derived class instance in derived class source code file.
        /// Use the 'new' keyword to overwrite default behaviour.
        /// </summary>
        /// <returns></returns>
        public new static IContainers GetInstance()
        {
            return new Containers();
        }

        public override int countCombinations(int fuelVolume, List<int> containers)
        {
            
            return 1;
        }
    }
}