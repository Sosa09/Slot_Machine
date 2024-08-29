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
        public static void ShowGameMinimumRequirement()
        {
            //BET MIN 1$ max 3$
            Console.WriteLine($"A minimum bet of {Constants.MINIMUM_BET} is required to spin.\n" + 
                              $"You'll earn {Constants.GAIN} per winning slot");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int GetGamerBet()
        {
            string playerBet = string.Empty;
            bool gamerBetValid= false;
            while (!gamerBetValid)
            {
                playerBet = Console.ReadLine();
                gamerBetValid = GamerBetValidated(playerBet);
            }
            return Convert.ToInt32(playerBet);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="possiblePlayDirection"></param>
        public static void DisplayGamePossibilities(string[] possiblePlayDirection)
        {
          
            //possiblePlayDirections are Horizontal, Vertical or Diagonal
            for (int i = 0; i < possiblePlayDirection.Length; i++)
            {
                Console.WriteLine($"{i}: {possiblePlayDirection[i]}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="possiblePlayDirection"></param>
        /// <returns></returns>
        public static PlayDirection GetGamerDirection(string[] possiblePlayDirection)
        {
            bool gamerDirectionChoiceValid = false;
            char gamerDirectionChoice = new char();

            while (!gamerDirectionChoiceValid)
            {
                gamerDirectionChoice = Console.ReadKey(true).KeyChar;
                gamerDirectionChoiceValid = GamerDirectionChoiceValidated(gamerDirectionChoice);
            }

            if (gamerDirectionChoice == Constants.HORIZONTAL)
                return PlayDirection.Horizontal;
            else if (gamerDirectionChoice == Constants.VERTICAL)
                return PlayDirection.Vertical;
            else
                return PlayDirection.Diagonal;
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gamerDirectionChoice"></param>
        /// <returns></returns>
        private static bool GamerDirectionChoiceValidated(int gamerDirectionChoice)
        {
            //validating user input and checking if choice is inside valid possible range
            while (gamerDirectionChoice >= Constants.MAX_PLAY_DIRECTIONS)
            {
                //error displayed if user choice is ouside of range or not a valid digit
                Console.WriteLine($"Please enter a valid direction choice");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="randomSlotNumbers"></param>
        public static void DisplayGrid(int[,] grid, int[] randomSlotNumbers)
        {
            Console.Clear();
            int index = 0;     
            for (int i = 0; i < Constants.GRID_ROW; i++)
            {
                for (int j = 0; j < Constants.GRID_COL; j++)
                {
                    grid[i, j] = randomSlotNumbers[index];       //TODO Think of a way of implementing this into logic
                    Console.Write($"{randomSlotNumbers[index]} ");
                    index++;
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static void DisplayWinningSlotLine()
        {
            Console.WriteLine($"you won {Constants.GAIN}$");
        }
        /// <summary>
        /// 
        /// </summary>
        public static void DisplaySlotResult(int totalWinnerLines,int total)
        {
            if (totalWinnerLines > 0)
                Console.WriteLine($"Amazing, you just made {totalWinnerLines}. with a ${total} bet");
            else
                Console.WriteLine($"Damn, you lost your bet {total}");
        }
        /// <summary>
        /// 
        /// </summary>
        public static void DisplayEndMessage(int profit)
        {
            Console.WriteLine($"you quitted the game with {profit}. see you");
        }
    }
}
