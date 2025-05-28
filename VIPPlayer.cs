using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roulette_Huang
{
    public class VIPPlayer : BasePlayer
    {
        public double BonusMultiplier { get; } = 1.1;

        public VIPPlayer(string name, int balance) : base(name, balance) { }

        public override void Win(int amount)
        {
            Balance += (int)(amount * BonusMultiplier);
        }
    }
}
