using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roulette_Huang
{
    public class RouletteWheel
    {
        public List<RouletteNumber> Numbers { get; }
        private Random random = new Random();

        public RouletteWheel()
        {
            Numbers = new List<RouletteNumber>();
            for (int i = 0; i <= 36; i++)
            {
                Numbers.Add(new RouletteNumber(i));
            }
        }

        public RouletteNumber Spin()
        {
            int index = random.Next(Numbers.Count);
            return Numbers[index];
        }
    }
}
