using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slot_Machine
{
    public static class UserInterface
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gamersWallet"></param>
        /// <param name="profit"></param>
        public static void GetGamerCurrentPlayStatus(int gamersWallet, int profit)
        {
            //Displaying player's total money and profit
            Console.WriteLine($"Your total money : {gamersWallet}");
            Console.WriteLine($"Your total profit: {profit}\n");

        }
        /// <summary>
        /// 
        /// </summary>
        public static void ShowGameMinimumRequirment()
        {
            //BET MIN 1$ max 3$
            Console.WriteLine($"A minimum bet of {Constants.MINIMUM_BET} is required to spin.\n" + 
                               "You'll earn {Constants.GAIN}$ per winning slot");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int GetGamerBet()
        {
            int playerBet = 0;
            bool betNotValid = Constants.BETNOTVALID;
            while (betNotValid)
            {
                string gamerBet = Console.ReadLine();
                betNotValid = ValidateGamerBet(gamerBet);
            }
            return playerBet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gamerBet"></param>
        /// <returns></returns>
        private static bool ValidateGamerBet(string gamerBet)
        {           
            if (!int.TryParse(gamerBet, out int gamerBetParsed))
            {
                Console.WriteLine($"{gamerBetParsed} please enter a valid number\nplease try again!");
                return Constants.BETNOTVALID;
            }
            else if (gamerBetParsed < Constants.MINIMUM_BET)
            {
                Console.WriteLine($"Minimum bet is {Constants.MINIMUM_BET}");
                return Constants.BETNOTVALID;
            }
            return Constants.BETVALID;            
        }
    }
}
