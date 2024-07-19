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

            const int MAX_SPIN_VALUE = 3;
            const int FIRST_POSITION_OF_DIMENSIONAL_ARRAY = 0;
            const int SECOND_POSITION_OF_DIMENSIONAL_ARRAY = 0;
            const int START_MONEY = 30; //virtual money every gamer starts with 

            const int MINIMUM_BET = 1;
            const int MAXIMUM_BET = 3;

            const int GRID_ROW = 3;
            const int GRID_COL = 3;

            int playerMoney = START_MONEY; //assigning the start money right away, it will hold the total money after the game
            int profite = 0; //loses or winnings of the users


            bool winningRow = false;
            bool isPlayerMoneyNotZero = true; //return true if user has no money to player anymore and will end the game

            //FIRST WHILE LOOP TO KEEP THE GAME RUNNING AFTER EACH PLAY
            while (true)
            {
                //ALONG THE GAME THE SYSTEM WILL CHECK IF USER HAS ENOUGH MONEY TO PLAY
                while (isPlayerMoneyNotZero)
                {
                    //BET MIN 1$ max 3$
                    Console.WriteLine($"Please bet minimum {MINIMUM_BET} or max {MAXIMUM_BET} dollars to spin");

                    if (int.TryParse(Console.ReadLine(), out int playerBet))
                    {

                        int[,] slot = new int[GRID_ROW, GRID_COL];

                        //looping trough the array.
                        //going throuhg the first dimension and then the second where we store the value to the second dimensional array for later check and print the value to the end user


                        for (int i = 0; i < slot.GetLength(FIRST_POSITION_OF_DIMENSIONAL_ARRAY); i++)
                        {

                            for (int j = 0; j < slot.GetLength(SECOND_POSITION_OF_DIMENSIONAL_ARRAY); j++)
                            {
                                int randomNr = random.Next(MAX_SPIN_VALUE);
                                slot[i, j] = randomNr;
                                Console.Write($"{randomNr} ");

                            }

                            Console.WriteLine();

                        }


                        //lloping to see if there is a winning row REPITITIVE CODE BAD and MAGICNUMBERS
                        for (int i = 0; i < slot.GetLength(FIRST_POSITION_OF_DIMENSIONAL_ARRAY); i++)
                        {
                            if (slot[i, 0] == slot[i, 1] && slot[i, 0] == slot[i, 2])
                                winningRow = true;

                        }

                        if (winningRow)
                        {
                            //add 1 dollar to profilte and to pllayer monay

                            profite++;
                            playerMoney++;

                            Console.WriteLine($"you won, your actual profit is {profite} and total money is {playerMoney}");
                        }
                        else
                        {
                            playerMoney--;
                            profite--;
                            Console.WriteLine($"you lost, your actual profit is {profite} and total money is {playerMoney}");

                        }


                    }
                    else
                    {
                        Console.WriteLine($"{playerBet} is not a valid number, please try again!");
                    }

                  


                }
                Console.WriteLine($"you quitted the game with {profite}. see you");
            }
        }
    }
}