using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roulette_Huang
{
    public class RouletteNumber
    {
        public int Number { get; }
        public ColorType Color { get; }

        public RouletteNumber(int number)
        {
            Number = number;
            Color = GetColor(number);
        }

        private ColorType GetColor(int number)
        {
            if (number == 0) return ColorType.Green;

            int[] red = { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };
            return red.Contains(number) ? ColorType.Red : ColorType.Black;
        }
    }
}
