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
            
            int firstValue = 0;
            int lastValue = 0;
            
            int wallet = Constants.START_MONEY; //assigning the start money right away, it will hold the total money after the game
            int profit = 0; //loses or winnings of the users

            bool isPlayerMoneyNotZero = true; //return true if user has no money to player anymore and will end the game

            //set up the winning choices
            string[] possiblePlayDirections = { PlayDirection.Horizontal.ToString(), PlayDirection.Vertical.ToString(), PlayDirection.Diagonal.ToString() };
            int[,] grid = new int[Constants.GRID_ROW, Constants.GRID_COL];
            //FIRST WHILE LOOP TO KEEP THE GAME RUNNING AFTER EACH PLAY
            while (true)
            {                
                //ALONG THE GAME THE SYSTEM WILL CHECK IF USER HAS ENOUGH MONEY TO PLAY
                while (isPlayerMoneyNotZero)
                {                    
                    if(wallet < Constants.MINIMUM_BET)
                        isPlayerMoneyNotZero = false;
                    bool winner = true;
                    int winningLines = 0; //will hold all the dollar for each loop and add it to the total profit
                  
                    UserInterface.GetGamerCurrentPlayStatus(wallet, profit);
                    UserInterface.ShowGameMinimumRequirement();
                                 
                    int playerBet = UserInterface.GetGamerBet();

                    UserInterface.DisplayGamePossibilities(possiblePlayDirections);

                    //Ask user to input his choice and store it in choice
                    PlayDirection gamerPlayDirectionChoice = UserInterface.GetGamerDirection(possiblePlayDirections);

                    //LOGIC Generate random numbers for the grid 0 and 1 s
                    int[] randomNumbers = Logic.GenerateSlotNumbers(random, Constants.TOTAL_GRID_CELLS);

                    UserInterface.DisplayGrid(grid, randomNumbers);

                    if (gamerPlayDirectionChoice == PlayDirection.Horizontal)
                    {
                        for (int i = 0; i < Constants.GRID_ROW; i++)
                        {
                            winner = true; //reset winner
                            firstValue = grid[i, 0];

                            //looping through the rest of the row since first value is the comparableNumber
                            for (int j = 1; j < Constants.GRID_COL; j++)
                            {
                                int currentCell = grid[i, j];
                                if (currentCell != firstValue)
                                {
                                    winner = false;
                                    break;
                                }                 
                            }
                            if (winner)
                            {
                                UserInterface.DisplayWinningSlotLine();                        
                                winningLines++;
                            }
                        }
                    }
                    else if (gamerPlayDirectionChoice == PlayDirection.Vertical)
                    {
                        //looping trough each colomn
                        for (int i = 0; i < Constants.GRID_COL; i++)
                        {
                            //storing the first element of each column(0, i);
              
                            winner = true;
                            firstValue = grid[0, i];
                            for (int j = 0; j < Constants.GRID_ROW; j++)
                            {
                                if (grid[j, i] != firstValue)
                                {
                                    winner = false;
                                    break;
                                }                            
                            }
                            if (winner)
                            {
                                UserInterface.DisplayWinningSlotLine();
                                winningLines++;
                            }
                        }
                    }
                    else if (gamerPlayDirectionChoice == PlayDirection.Diagonal)
                    {
                        
                        firstValue = grid[grid.GetLowerBound(0), grid.GetLowerBound(1)];
                        lastValue = grid[grid.GetLowerBound(0), grid.GetUpperBound(0)];
                        
                        for (int i = 1; i < Constants.GRID_ROW; i++)
                        {
                            var actualValue = grid[i, i];
                            if (actualValue != firstValue)
                            {
                                winner = false;
                                break;
                            }
                        }
                        if (winner)
                        {
                            UserInterface.DisplayWinningSlotLine();
                            winningLines++;
                        }
                        winner = true;
                        int currentCol = 0;

                        for (int i = grid.GetUpperBound(0); i >= 0; i--)
                        {
                    
                            var actualValue = grid[i, currentCol];
                            
                            if (actualValue != lastValue)
                            {
                                winner = false;
                                break;
                            }
                            currentCol++;                                    
                        }
                        if (winner)
                        {
                            UserInterface.DisplayWinningSlotLine();
                            winningLines++;
                        }
                    }

                    //checking if player has won something
                    if (winningLines > 0)
                    {                        
                        profit += winningLines;
                        wallet += winningLines + playerBet;
                        UserInterface.DisplaySlotResult(winningLines, profit);
                    }
                    else
                    {                        
                        profit -= winningLines;
                        wallet -= playerBet;
                        UserInterface.DisplaySlotResult(winningLines, playerBet);
                    }
                }

                UserInterface.DisplayEndMessage(profit);
            }      
        }
    }
}