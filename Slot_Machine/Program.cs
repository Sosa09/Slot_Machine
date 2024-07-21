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

            const string HORIZONTAL_CHOICE = "Horizaontal";
            const string VERTICAL_CHOICE = "Vertical";
            const string DIAGONAL_CHOICE = "Diagonal";

      
            const int FIRST_POSITION_OF_DIMENSIONAL_ARRAY = 0;
            const int SECOND_POSITION_OF_DIMENSIONAL_ARRAY = 0;
            const int START_MONEY = 30; //virtual money every gamer starts with 

            const int MINIMUM_BET = 3;
            

            const int GRID_ROW = 3;
            const int GRID_COL = 3;

            //corresponds to the compoarable indices in order to check for a winning spin
    
            const int FIRT_LINE_ELEMENT = 0;
            const int MIDDLE_LNE_ELEMENT = 1;
            
            const int GAIN = 1;

            int playerMoney = START_MONEY; //assigning the start money right away, it will hold the total money after the game
            int profit = 0; //loses or winnings of the users

            bool isPlayerMoneyNotZero = true; //return true if user has no money to player anymore and will end the game

            //FIRST WHILE LOOP TO KEEP THE GAME RUNNING AFTER EACH PLAY
            while (true)
            {
                //ALONG THE GAME THE SYSTEM WILL CHECK IF USER HAS ENOUGH MONEY TO PLAY
                while (isPlayerMoneyNotZero)
                {
                    //Displaying player's total money and profit
                    Console.WriteLine($"Your total money: {playerMoney}");
                    Console.WriteLine($"Your total profit: {profit}\n");
                    //BET MIN 1$ max 3$
                    Console.WriteLine($"Please bet minimum {MINIMUM_BET} dollars to spin, you'll earn 1$ per winning slot");

                    //validate user input
                    if (int.TryParse(Console.ReadLine(), out int playerBet))
                    {
                        //check if player has ennough money to play and if he has entered the minimum bet
                        if(playerBet >= MINIMUM_BET || playerMoney < playerBet)
                        {
                            //define the grid
                            int[,] slot = new int[GRID_ROW, GRID_COL];

                            //set up the choices
                            string[] possibleChoices = { HORIZONTAL_CHOICE, VERTICAL_CHOICE, DIAGONAL_CHOICE };

                            for (int i = 0; i < possibleChoices.Length; i++)
                            {
                                Console.WriteLine($"{i}: {possibleChoices[i]}");
                            }

                            //ask the player to bet on a row or col horizontally, vertically, diagonally
                            char choice = Console.ReadKey(false).KeyChar; //adding false as arg to readkey to not print the user choice
                            Console.WriteLine();

                            //Display the grid with the random generated numbers
                            for (int i = 0; i < GRID_ROW; i++)
                            {
                                for (int j = 0; j < GRID_COL; j++)
                                {
                                    int randomNr = random.Next(2);
                                    slot[i, j] = randomNr;
                                    Console.Write($"{randomNr} ");
                                }

                                Console.WriteLine();

                            }

                            bool winner = false;
                            int spinGain = 0;
                            int comparableNumber = 0;
                            if (possibleChoices[int.Parse(choice.ToString())] == HORIZONTAL_CHOICE)
                            {

                                for (int i = 0; i < GRID_ROW; i++)
                                {
                                    comparableNumber = slot[i, FIRT_LINE_ELEMENT];

                                    for (int j = 0; j < GRID_COL; j++)
                                    {
                                        int currentCell = slot[i, j];
                                        if (currentCell != comparableNumber)
                                        {
                                            winner = false;
                                            break;

                                        }
                                        winner = true;
                                    }
                                    if(winner)
                                    {
                                        Console.WriteLine($"you won {GAIN}$");
                                        spinGain++;
                                    }
                                        
                                }
      
                            }
                            else if (possibleChoices[int.Parse(choice.ToString())] == VERTICAL_CHOICE)
                            {
                                for (int i = 0; i < GRID_ROW; i++)
                                {
                                    comparableNumber = slot[FIRT_LINE_ELEMENT, i];
                                    for (int j = 0; j < GRID_COL; j++)
                                    {
                                        if (slot[j, i] != comparableNumber)
                                        {
                                            winner = false;
                                            break;
                                        }

                                        winner = true;
                                    }
                                    if (winner)
                                    {
                                        Console.WriteLine($"you won {GAIN}$");
                                        spinGain++;
                                       
                                    }
                                }
                            }
                            else if (possibleChoices[int.Parse(choice.ToString())] == DIAGONAL_CHOICE)
                            {
                                comparableNumber = slot[MIDDLE_LNE_ELEMENT, 1]; //store middle for comparission
                                int currentCell = 0;
                                int actualIndex = 0; //increases or decrease by one depending on the flow of verficatio for instance if we check from right to left the actualInde should be 2 and will decrease to 1
                                for (int i = 0; i < GRID_ROW; i++)//since int is a value type it gets its own place int he tack nd will not share the same ref as actualIndex
                                {
                                    
                                    currentCell = slot[i, actualIndex];

                                    if (comparableNumber != currentCell)
                                        winner = false;
                                    else
                                        winner = true;

                                    actualIndex = i;

                                    if (winner)
                                    {
                                        Console.WriteLine($"you won {GAIN}$");
                                        spinGain++;
                                    }

                                }
                                //checking reverse diagonal
                                //actual index will bebgin with 3 here
                                for (uint i = 2; GRID_ROW > i; i--)
                                {
                                    currentCell = slot[i, actualIndex];

                                    if (comparableNumber != currentCell)
                                        winner = false;
                                    else
                                        winner = true;

                                    actualIndex = (int)i;

                                    if (winner)
                                    {
                                        Console.WriteLine($"you won {GAIN}$");
                                        spinGain++;
                                    }

                                }
                            }
                            else
                            {
                                Console.WriteLine("An error occured please try again.");
                            }

                            //checking if player has won something
                            if (spinGain > 0)
                            {
                                Console.WriteLine($"your total gain for this slot is {spinGain}");

                                profit += spinGain;
                                playerMoney += spinGain;

                            }
                            else
                            {
                                Console.WriteLine($"you lost {playerBet}");
                                playerMoney -= playerBet;
                                profit -= spinGain;
                            }
                            Console.WriteLine();
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