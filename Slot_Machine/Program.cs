using System.Data.SqlTypes;
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

            const int MAX_SPIN_VALUE = 3;
            const int FIRST_POSITION_OF_DIMENSIONAL_ARRAY = 0;
            const int SECOND_POSITION_OF_DIMENSIONAL_ARRAY = 0;
            const int START_MONEY = 30; //virtual money every gamer starts with 

            const int MINIMUM_BET = 3;
            

            const int GRID_ROW = 3;
            const int GRID_COL = 3;
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
                        if(playerBet >= 3)
                        {
                            int[,] slot = new int[GRID_ROW, GRID_COL];

                            
                            string[] possibleChoices = { HORIZONTAL_CHOICE, VERTICAL_CHOICE, DIAGONAL_CHOICE };

                            for (int i = 0; i < possibleChoices.Length; i++)
                            {
                                Console.WriteLine($"{i}: {possibleChoices[i]}");
                            }

                            //ask the player to bet on a row or col horizontally, vertically, diagonally
                            char choice = Console.ReadKey().KeyChar;
                            Console.WriteLine();

                            //Display the grid with the random generated numbers
                            for (int i = 0; i < slot.GetLength(FIRST_POSITION_OF_DIMENSIONAL_ARRAY); i++)
                            {
                                for (int j = 0; j < slot.GetLength(SECOND_POSITION_OF_DIMENSIONAL_ARRAY); j++)
                                {
                                    int randomNr = random.Next(2);
                                    slot[i, j] = randomNr;
                                    Console.Write($"{randomNr} ");
                                }

                                Console.WriteLine();

                            }

                            bool winner = false;
                            if (possibleChoices[int.Parse(choice.ToString())] == HORIZONTAL_CHOICE)
                            {

                                for (int i = 0; i < GRID_ROW; i++)
                                {
                                    int nrToFind = slot[i, 0];

                                    for (int j = 0; j < GRID_COL; j++)
                                    {
                                        if (slot[i, j] != nrToFind)
                                        {
                                            winner = false;
                                            break;

                                        }
                                        winner = true;

                                    }
                                    if (winner)
                                    {

                                        playerMoney = profit + GAIN;
                                        Console.WriteLine($"you won {GAIN}$");
                                    }
                                }
                            }
                            else if (possibleChoices[int.Parse(choice.ToString())] == VERTICAL_CHOICE)
                            {
                                for (int i = 0; i < GRID_ROW; i++)
                                {
                                    int nrToFind = slot[0, i];
                                    for (int j = 0; j < GRID_COL; j++)
                                    {
                                        if (slot[j, i] != nrToFind)
                                        {
                                            winner = false;
                                            break;
                                        }

                                        winner = true;
                                    }
                                    if (winner)
                                    {

                                        playerMoney = profit + GAIN;
                                        Console.WriteLine($"you won {GAIN}$");
                                    }
                                }
                            }
                            else if (possibleChoices[int.Parse(choice.ToString())] == DIAGONAL_CHOICE)
                            {

                            }
                            else
                            {
                                Console.WriteLine("An error occured please try again.");
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