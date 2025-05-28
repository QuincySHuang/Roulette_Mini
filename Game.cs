using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roulette_Huang
{
    public class Game
    {
        public RouletteWheel Wheel { get; } = new RouletteWheel();
        public List<Bet> BetHistory { get; } = new List<Bet>();
        public RouletteNumber LastSpinResult { get; private set; }

        public (RouletteNumber, string) PlayRound(BasePlayer player, Bet bet)
        {
            var result = Wheel.Spin();
            Console.WriteLine($"Wheel landed on {result.Number} ({result.Color})");

            bool isWin = CheckWin(bet, result);
            int payout = isWin ? bet.Amount * GetPayoutMultiplier(bet.Type) : 0;
            string message; 

            if (isWin)
            {
                player.Win(payout);
                message = ($"You win ${payout}!");
            }
            else
            {
                player.Lose(bet.Amount);
                message = ("You lost the bet.");
            }

            BetHistory.Add(bet);
            return (result, message);
        }

        private bool CheckWin(Bet bet, RouletteNumber result)
        {
            switch (bet.Type)
            {
                case BetType.Number:
                    return result.Number.ToString() == bet.Value;

                case BetType.Color:
                    //return result.Color.ToString().Equals(bet.Value, StringComparison.OrdinalIgnoreCase);
                    return string.Equals(result.Color.ToString().Trim(), bet.Value.Trim(), StringComparison.OrdinalIgnoreCase);


                case BetType.Dozen:
                    int n = result.Number;
                    return (bet.Value == "1st" && n >= 1 && n <= 12) ||
                           (bet.Value == "2nd" && n >= 13 && n <= 24) ||
                           (bet.Value == "3rd" && n >= 25 && n <= 36);

                case BetType.Range:
                    return (bet.Value == "1-18" && result.Number >= 1 && result.Number <= 18) ||
                           (bet.Value == "19-36" && result.Number >= 19 && result.Number <= 36);

                case BetType.OddEven:
                    if (result.Number == 0) return false;
                    return (bet.Value == "Odd" && result.Number % 2 != 0) ||
                           (bet.Value == "Even" && result.Number % 2 == 0);

                default:
                    return false;
            }
        }

        private int GetPayoutMultiplier(BetType type)
        {
            int multiplier;

            switch (type)
            {
                case BetType.Number:
                    multiplier = 35;
                    break;
                case BetType.Dozen:
                    multiplier = 3;
                    break;
                case BetType.Range:
                    multiplier = 2;
                    break;
                case BetType.OddEven:
                    multiplier = 2;
                    break;
                case BetType.Color:
                    multiplier = 2;
                    break;
                default:
                    multiplier = 0;
                    break;
            }

            return multiplier;
        
        }
    }
}