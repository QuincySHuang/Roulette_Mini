using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roulette_Huang
{
    public abstract class BasePlayer
    {
        public string Name { get; set; }
        public int Balance { get; set; }

        public BasePlayer(string name, int balance)
        {
            Name = name;
            Balance = balance;
        }

        public virtual void Win(int amount)
        {
            if (amount > 0)
            {
                Balance += amount;
            }
        }

        public virtual void Lose(int amount)
        {
            if (amount > 0 && amount <= Balance)
            {
                Balance -= amount;
            }
        }
    }
}
