using Microsoft.VisualBasic;
using System.CodeDom.Compiler;
using System.Data;
using System.Data.SqlTypes;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using System.Security.Cryptography;

namespace Slot_Machine
{
    public class Program
    {
        public enum PlayDirection
        {
            Horizontal,
            Vertical,
            Diagonal
        }
        static void Main(string[] args)
        {
            //instantiating Random class to get random numbers for the slot machine
            Random random = new Random();
            
            int wallet = Constants.START_MONEY; //assigning the start money right away, it will hold the total money after the game
            int profit = 0; //loses or winnings of the users

            bool PlayerHasMoney = true; //return true if user has no money to player anymore and will end the game

            //set up the winning choices
            string[] possiblePlayDirections = { PlayDirection.Horizontal.ToString(), PlayDirection.Vertical.ToString(), PlayDirection.Diagonal.ToString() };
            int[,] grid = new int[Constants.GRID_ROW, Constants.GRID_COL];
            //FIRST WHILE LOOP TO KEEP THE GAME RUNNING AFTER EACH PLAY
            while (true)
            {                
                //ALONG THE GAME THE SYSTEM WILL CHECK IF USER HAS ENOUGH MONEY TO PLAY
                while (PlayerHasMoney)
                {                    
                    if(wallet < Constants.MINIMUM_BET)
                        PlayerHasMoney = false;
                
                    int winnerLineCount = 0;
                  
                    UserInterface.GetGamerCurrentPlayStatus(wallet, profit);
                    UserInterface.ShowGameMinimumRequirement();
                                 
                    int playerBet = UserInterface.GetGamerBet();

                    UserInterface.DisplayGamePossibilities(possiblePlayDirections);

                    PlayDirection gamerPlayDirectionChoice = UserInterface.GetGamerDirection();

                    int[] randomNumbers = Logic.GenerateSlotNumbers(random, Constants.TOTAL_GRID_CELLS);

                    UserInterface.DisplayGrid(grid, randomNumbers);

                    switch (gamerPlayDirectionChoice)
                    {
                        case PlayDirection.Horizontal:
                            winnerLineCount = Logic.GetHorizontalTotalWinningAreas(grid);
                            break;
                        case PlayDirection.Vertical:
                            winnerLineCount = Logic.GetVerticalTotalWinningAreas(grid);
                            break;
                        case PlayDirection.Diagonal:
                            winnerLineCount = Logic.GetDiagonalTotalWinningAreas(grid);
                            break;
                        default:
                            break;
                    }
                    //checking if player has won something
                    Logic.UpdateGamerWallet(winnerLineCount, playerBet, ref profit, ref wallet);

                    UserInterface.DisplaySlotResult(winnerLineCount, playerBet);
                }
                UserInterface.DisplayEndMessage(profit);
            }                 
        }
    }
}