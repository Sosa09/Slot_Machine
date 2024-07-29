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

            //Defining winning line possibilities
            const string HORIZONTAL_CHOICE = "Horizaontal";
            const string VERTICAL_CHOICE = "Vertical";
            const string DIAGONAL_CHOICE = "Diagonal";

         
            const int START_MONEY = 30; //virtual money every gamer starts with 
            const int MINIMUM_BET = 3; //Minimum bet



            const int DIFFICULTY = 2; //Which is easy. random will generate nr in grid between 0 and 2

            //Defining Grid ROW and COL Size
            const int GRID_ROW = 3;
            const int GRID_COL = 3;

            const int FIRST_VALUE = 0;//Will be used to get the first value of the 2d array (FIRST_VALUE,FIRST_VALUE)
            const int LAST_ROW_VALUE = GRID_ROW;//Will be used to get the first value of the 2d array (FIRST_VALUE,FIRST_VALUE)
            const int LAST_COL_VALUE = GRID_COL;//Will be used to get the first value of the 2d array (FIRST_VALUE,FIRST_VALUE)


            const int GAIN = 1;//Total dollar per winning line

            int playerMoney = START_MONEY; //assigning the start money right away, it will hold the total money after the game
            int profit = 0; //loses or winnings of the users

            bool isPlayerMoneyNotZero = true; //return true if user has no money to player anymore and will end the game
            //Define the GRID
            int[,] grid = new int[GRID_ROW, GRID_COL];

            //set up the winning choices
            string[] possibleChoices = { HORIZONTAL_CHOICE, VERTICAL_CHOICE, DIAGONAL_CHOICE };

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
                    Console.WriteLine($"Please bet minimum {MINIMUM_BET} dollars to spin, you'll earn {GAIN}$ per winning slot");

             
                    //resetting player bet
                    int playerBet = 0;
                    //Validating playerbet input should be 3           
                    while (playerBet < MINIMUM_BET)
                    {
                        //Validate userinput must be a valid int only shown if userinput not an int
                        if(!int.TryParse(Console.ReadLine(), out playerBet))
                        {
                            Console.WriteLine($"{playerBet} please enter a valid number\n" +
                                                          $"please try again!");
                   
                        }
                        
                        Console.WriteLine($"Minimum bet is {MINIMUM_BET}");
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
                    int gainingLines = 0; //will hold all the dollar for each loop and add it to the total profit
                    int comparableNumber = 0;

                    if (userChoice == HORIZONTAL_CHOICE)
                    {

                        for (int i = 0; i < GRID_ROW; i++)
                        {
                            comparableNumber = grid[i, FIRST_VALUE];
                            winner = true;
                            //looping through the rest of the row since first value is the comparableNumber
                            for (int j = 1; j < GRID_COL; j++)
                            {
                                int currentCell = grid[i, j];
                                if (currentCell != comparableNumber)
                                {
                                    winner = false;
                                    break;

                                }
                 
                            }
                            if (winner)
                            {
                                Console.WriteLine($"you won {GAIN}$");
                                gainingLines++;
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
                            winner = true;
                            for (int j = 1; j < GRID_ROW; j++)
                            {
                                if (grid[j, i] != comparableNumber)
                                {
                                    winner = false;
                                    break;
                                }

                            
                            }
                            if (winner)
                            {
                                Console.WriteLine($"you won {GAIN}$");
                                gainingLines++;

                            }
                        }
                    }
                    else if (userChoice == DIAGONAL_CHOICE)
                    {
                        int firstElement = grid[FIRST_VALUE, FIRST_VALUE];
                        int lastElement = grid[FIRST_VALUE, 2];
                        //DIAGONAL CHECK
                        for (int i = 1; i < GRID_ROW; i++)//since int is a value type it gets its own place int he tack nd will not share the same ref as actualIndex
                        {

                            if (grid[i, i] != firstElement)
                                winner = false;
                        }
                        if (winner)
                        {
                            Console.WriteLine($"you won {GAIN}$");
                            gainingLines++;
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