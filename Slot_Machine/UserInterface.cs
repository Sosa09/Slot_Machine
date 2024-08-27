using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Slot_Machine.Program;

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
            bool gamerBetValidated = Constants.BETNOTVALID;
            while (gamerBetValidated)
            {
                string gamerBet = Console.ReadLine();
                gamerBetValidated = GamerBetValidated(gamerBet);
            }
            return playerBet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gamerBet"></param>
        /// <returns></returns>
        private static bool GamerBetValidated(string gamerBet)
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

        public static void DisplayGamePossibilities(string[] possiblePlayDirection)
        {
            //Display the winning choices
            //possiblePlayDirections are Horizontal, Vertical or Diagonal
            for (int i = 0; i < possiblePlayDirection.Length; i++)
            {
                Console.WriteLine($"{i}: {possiblePlayDirection[i]}");
            }
        }

        public static PlayDirection GetGamerDirection(string[] possiblePlayDirection)
        {
            bool gamerDirectionChoiceNotValid = true;
            char gamerDirectionChoice = new char();
            while (gamerDirectionChoiceNotValid)
            {
                gamerDirectionChoice = Console.ReadKey(false).KeyChar;
                gamerDirectionChoiceNotValid = GamerDirectionChoiceValidated(gamerDirectionChoice);
            }

            if (gamerDirectionChoice == Constants.HORIZONTAL)
                return PlayDirection.Horizontal;
            else if (gamerDirectionChoice == Constants.VERTICAL)
                return PlayDirection.Vertical;
            else
                return PlayDirection.Diagonal;
            
        }

        private static bool GamerDirectionChoiceValidated(int gamerDirectionChoice)
        {
            //validating user input and checking if choice is inside valid possible range
            while (gamerDirectionChoice >= Constants.MAX_PLAY_DIRECTIONS)
            {
                //error displayed if user choice is ouside of range or not a valid digit
                Console.WriteLine($"Please enter a valid direction choice, {gamerDirectionChoice} is invalid.");
                return false;
            }
            return true;
        }


    }
}
