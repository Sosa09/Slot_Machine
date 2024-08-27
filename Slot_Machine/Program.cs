using Microsoft.VisualBasic;
using System.Data.SqlTypes;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using System.Security.Cryptography;

namespace Slot_Machine
{
    internal class Program
    {

        static void Main(string[] args)
        {
            //instantiating Random class to get random numbers for the slot machine
            Random random = new Random();
            

            int firstValue = 0;
            int lastValue = 0;
            

            int playerMoney = Constants.START_MONEY; //assigning the start money right away, it will hold the total money after the game
            int profit = 0; //loses or winnings of the users

            bool isPlayerMoneyNotZero = true; //return true if user has no money to player anymore and will end the game


            //set up the winning choices
            string[] possibleChoices = { Constants.HORIZONTAL_CHOICE, Constants.VERTICAL_CHOICE, Constants.DIAGONAL_CHOICE };
            int[,] grid = new int[Constants.GRID_ROW, Constants.GRID_COL];
            //FIRST WHILE LOOP TO KEEP THE GAME RUNNING AFTER EACH PLAY
            while (true)
            {
                //ALONG THE GAME THE SYSTEM WILL CHECK IF USER HAS ENOUGH MONEY TO PLAY
                while (isPlayerMoneyNotZero)
                {
                    if(playerMoney < Constants.MINIMUM_BET)
                        isPlayerMoneyNotZero = false;

                    //Displaying player's total money and profit
                    Console.WriteLine($"Your total money: {playerMoney}");
                    Console.WriteLine($"Your total profit: {profit}\n");

                    //BET MIN 1$ max 3$
                    Console.WriteLine($"Please bet minimum {Constants.MINIMUM_BET} dollars to spin, you'll earn {Constants.GAIN}$ per winning slot");

             
                    //resetting player bet
                    int playerBet = 0;

                    //Validating playerbet input should be 3           
                    while (playerBet < Constants.MINIMUM_BET)
                    {
                        //Validate userinput must be a valid int only shown if userinput not an int
                        if(!int.TryParse(Console.ReadLine(), out playerBet))
                        {
                            Console.WriteLine($"{playerBet} please enter a valid number\n" +
                                                          $"please try again!");
                   
                        }
                        
                        Console.WriteLine($"Minimum bet is {Constants.MINIMUM_BET}");
                    }



                    //Display the winning choices
                    //possibleChoices are Horizontal, Vertical or Diagonal
                    for (int i = 0; i < possibleChoices.Length; i++)
                    {
                        Console.WriteLine($"{i}: {possibleChoices[i]}");
                    }

                    //Ask user to input his choice and store it in choice
                    char choice = Console.ReadKey(false).KeyChar;
                    int index = 0;

                    //validating user input and checking if choice is inside valid possible range
                    while(!int.TryParse(choice.ToString(), out index) || index >= possibleChoices.Length)
                    {
                        //error displayed if user choice is ouside of range or not a valid digit
                        Console.WriteLine($"An error occured with your input {index} is invalid.");

                        //asking user to input his choice again
                        choice = Console.ReadKey(false).KeyChar;

                    }
                    
                    //storing user choice 
                    var userChoice = possibleChoices[index];
                    Console.WriteLine();

                    //Design the grid with the random generated numbers
                    for (int i = 0; i < Constants.GRID_ROW; i++)
                    {
                        for (int j = 0; j < Constants.GRID_COL; j++)
                        {
                            int randomNr = random.Next(Constants.DIFFICULTY);
                            grid[i, j] = randomNr;
                            Console.Write($"{randomNr} ");
                        }

                        Console.WriteLine();

                    }

                    bool winner = true;
                    int gainingLines = 0; //will hold all the dollar for each loop and add it to the total profit

    

                    if (userChoice == Constants.HORIZONTAL_CHOICE)
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
                    else if (userChoice == Constants.VERTICAL_CHOICE)
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
                    else if (userChoice == Constants.DIAGONAL_CHOICE)
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
                        playerMoney += gainingLines;

                    }
                    else
                    {
                        Console.WriteLine($"you lost your bet {playerBet}");
                        playerMoney -= playerBet;
                        profit -= gainingLines;
                    }
                    
                    Console.WriteLine();
                    
                }
                Console.WriteLine($"you quitted the game with {profit}. see you");
            }
        }
    }
}