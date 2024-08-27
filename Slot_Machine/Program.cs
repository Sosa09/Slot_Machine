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
                    int gainingLines = 0; //will hold all the dollar for each loop and add it to the total profit

                    //PLACEHOLDER SHOW PLAYER STATUS AND RULE !
                    UserInterface.GetGamerCurrentPlayStatus(wallet, profit);
                    UserInterface.ShowGameMinimumRequirment();
             
                    //resetting player bet
                    int playerBet = UserInterface.GetGamerBet();

                    UserInterface.DisplayGamePossibilities(possiblePlayDirections);

                    //Ask user to input his choice and store it in choice
                    PlayDirection gamerPlayDirectionChoice = UserInterface.GetGamerDirection(possiblePlayDirections);
 
                    //LOGIC Generate random numbers for the grid 0 and 1 s
                    int[] randomNumbers = new int[Constants.GRID_ROW * Constants.GRID_COL];
                    for (int i = 0; i < randomNumbers.Length; i++)
                    {
                        randomNumbers[i] = random.Next(Constants.DIFFICULTY);
                    }

                    UserInterface.DisplayGrid(grid, randomNumbers);

                    if (gamerPlayDirectionChoice == PlayDirection.Horizontal)
                    {

                        for (int i = 0; i < Constants.GRID_ROW; i++)
                        {                        
                            winner = true;
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
                                Console.WriteLine($"you won {Constants.GAIN}$");
                                gainingLines++;
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
                                Console.WriteLine($"you won {Constants.GAIN}$");
                                gainingLines++;
                            }
                        }
                    }
                    else if (gamerPlayDirectionChoice == PlayDirection.Diagonal)
                    {
                        //store the firstvalue from first row and last value from the first row needed for comparision in the different options
                        firstValue = grid[grid.GetLowerBound(0), grid.GetLowerBound(1)];
                        lastValue = grid[grid.GetLowerBound(0), grid.GetUpperBound(0)];
                        //DIAGONAL CHECK
                        for (int i = 1; i < Constants.GRID_ROW; i++)//since int is a value type it gets its own place int he tack nd will not share the same ref as actualIndex
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
                            Console.WriteLine($"you won {Constants.GAIN} left to right$");
                            gainingLines++;
                        }
                        winner = true;
                        int currentCol = 0;
                        //ANTI diagonal check
                        for (int i = grid.GetUpperBound(0); i >= 0; i--)//since int is a value type it gets its own place int he tack nd will not share the same ref as actualIndex
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
                            Console.WriteLine($"you won {Constants.GAIN} right to left$");
                            gainingLines++;
                        }
                    }

                    //checking if player has won something
                    if (gainingLines > 0)
                    {
                        Console.WriteLine($"Total win for this slot {gainingLines}");
                        profit += gainingLines;
                        wallet += gainingLines;
                    }
                    else
                    {
                        Console.WriteLine($"you lost your bet {playerBet}");
                        wallet -= playerBet;
                        profit -= gainingLines;
                    }
                    
                    Console.WriteLine();
                    
                }
                Console.WriteLine($"you quitted the game with {profit}. see you");
            }
        }
    }
}