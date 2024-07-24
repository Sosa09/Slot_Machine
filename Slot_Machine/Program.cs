using Microsoft.VisualBasic;
using System.Data.SqlTypes;
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

            //Defining winning line possibilities
            const string HORIZONTAL_CHOICE = "Horizaontal";
            const string VERTICAL_CHOICE = "Vertical";
            const string DIAGONAL_CHOICE = "Diagonal";

         
            const int START_MONEY = 30; //virtual money every gamer starts with 
            const int MINIMUM_BET = 3; //Minimum bet

            const int FIRST_VALUE= 0;//Will be used to get the first value of the 2d array (FIRST_VALUE,FIRST_VALUE)

            const int DIFFICULTY = 2; //Which is easy. random will generate nr in grid between 0 and 2

            //Defining Grid ROW and COL Size
            const int GRID_ROW = 3;
            const int GRID_COL = 3;
            
            
            const int GAIN = 1;//Total dollar per winning line

            int playerMoney = START_MONEY; //assigning the start money right away, it will hold the total money after the game
            int profit = 0; //loses or winnings of the users

            bool isPlayerMoneyNotZero = true; //return true if user has no money to player anymore and will end the game

            //FIRST WHILE LOOP TO KEEP THE GAME RUNNING AFTER EACH PLAY
            while (true)
            {
                //ALONG THE GAME THE SYSTEM WILL CHECK IF USER HAS ENOUGH MONEY TO PLAY
                while (isPlayerMoneyNotZero)
                {
                    if(playerMoney < MINIMUM_BET)
                        isPlayerMoneyNotZero = true;
                    //Displaying player's total money and profit
                    Console.WriteLine($"Your total money: {playerMoney}");
                    Console.WriteLine($"Your total profit: {profit}\n");
                    //BET MIN 1$ max 3$
                    Console.WriteLine($"Please bet minimum {MINIMUM_BET} dollars to spin, you'll earn 1$ per winning slot");

                    //validate user input
                    if (int.TryParse(Console.ReadLine(), out int playerBet))
                    {
                        //check if player has ennough money to play and if he has entered the minimum bet
                        if(playerBet >= MINIMUM_BET && playerMoney > playerBet)
                        {
                            //Define the GRID
                            int[,] grid = new int[GRID_ROW, GRID_COL];

                            //set up the winning choices
                            string[] possibleChoices = { HORIZONTAL_CHOICE, VERTICAL_CHOICE, DIAGONAL_CHOICE };

                            //Display the winning choices
                            for (int i = 0; i <= possibleChoices.Length; i++)
                            {
                                Console.WriteLine($"{i}: {possibleChoices[i]}");
                            }

                            //ask the player to bet on a row or col horizontally, vertically, diagonally
                            char choice = Console.ReadKey(false).KeyChar; //adding false as arg to readkey to not print the user choice
                            

                            //validating user input and checking if choice is inside valid possible range
                            if(int.TryParse(choice.ToString(), out int index) && index < possibleChoices.Length)
                            {

                                //storing user choice 
                                var userChoice = possibleChoices[index];
                                Console.WriteLine();

                                //Design the grid with the random generated numbers
                                for (int i = 0; i < GRID_ROW; i++)
                                {
                                    for (int j = 0; j < GRID_COL; j++)
                                    {
                                        int randomNr = random.Next(DIFFICULTY);
                                        grid[i, j] = randomNr;
                                        Console.Write($"{randomNr} ");
                                    }

                                    Console.WriteLine();

                                }

                                bool winner = true;
                                int gainingLine = 0; //will hold all the dollar for each loop and add it to the total profit
                                int comparableNumber = 0;
                                if (userChoice == HORIZONTAL_CHOICE)
                                {

                                    for (int i = 0; i < GRID_ROW; i++)
                                    {
                                        comparableNumber = grid[i, FIRST_VALUE];
                                        //looping through the rest of the row since first value is the comparableNumber
                                        for (int j = 1; j < GRID_COL; j++)
                                        {
                                            int currentCell = grid[i, j];
                                            if (currentCell != comparableNumber)
                                            {
                                                winner = false;
                                                break;

                                            }
                                            winner = true;
                                        }
                                        if (winner)
                                        {
                                            Console.WriteLine($"you won {GAIN}$");
                                            gainingLine++;
                                        }

                                    }

                                }
                                else if (userChoice == VERTICAL_CHOICE)
                                {
                                    //looping trough each colomn
                                    for (int i = 0; i < GRID_COL; i++)
                                    {
                                        //storing the first element of each column(0, i);
                                        comparableNumber = grid[FIRST_VALUE, i];

                                        for (int j = 1; j < GRID_ROW; j++)
                                        {
                                            if (grid[j, i] != comparableNumber)
                                            {
                                                winner = false;
                                                break;
                                            }

                                            winner = true;
                                        }
                                        if (winner)
                                        {
                                            Console.WriteLine($"you won {GAIN}$");
                                            gainingLine++;

                                        }
                                    }
                                }
                                else if (userChoice == DIAGONAL_CHOICE)
                                {
                                    int firstElement = grid[FIRST_VALUE, 0];
                                    int lastElement = grid[0, 2];
                                    //DIAGONAL CHECK
                                    for (int i = 1; i < GRID_ROW; i++)//since int is a value type it gets its own place int he tack nd will not share the same ref as actualIndex
                                    {

                                        if (grid[i, i] != firstElement)
                                            winner = false;
                                    }
                                    if (winner)
                                    {
                                        Console.WriteLine($"you won {GAIN}$");
                                        gainingLine++;
                                    }
                                    winner = true;
                                    //ANTI diagonal check
                                    for (int i = 2; i >= 1; i--)//since int is a value type it gets its own place int he tack nd will not share the same ref as actualIndex
                                    {

                                        if (grid[i, i] != lastElement)
                                            winner = false;
                                    }
                                    if (winner)
                                    {
                                        Console.WriteLine($"you won {GAIN}$");
                                        gainingLine++;
                                    }

                                }
                                else
                                {
                                    
                                    Console.WriteLine("An error occured please try again.");
                                }

                                //checking if player has won something
                                if (gainingLine > 0)
                                {
                                    Console.WriteLine($"your total gain for this slot is {gainingLine}");

                                    profit += gainingLine;
                                    playerMoney += gainingLine;

                                }
                                else
                                {
                                    Console.WriteLine($"you lost {playerBet}");
                                    playerMoney -= playerBet;
                                    profit -= gainingLine;
                                }
                                Console.WriteLine();
                            }
              
                        }
                        else
                        {
                            Console.WriteLine($"Minimum bet is {MINIMUM_BET}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{playerBet} is not a valid number, please try again!");
                    }
                }
                Console.WriteLine($"you quitted the game with {profit}. see you");
            }
        }
    }
}