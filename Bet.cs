using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roulette_Huang
{
    public class Bet
    {
        public BetType Type { get; set; }
        public string Value { get; set; }
        public int Amount { get; set; }

        public Bet(BetType type, string value, int amount)
        {
            Type = type;
            Value = value;
            Amount = amount;
        }

        public Bet() { }
    }
}

