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
            
            const int START_MONEY = 30; //virtual money every gamer starts with 
            int playerMoney = START_MONEY; //assigning the start money right away, it will hold the total money after the game
            int profite = 0; //loses or winnings of the users
            bool isPlayerMoneyZero = false; //return true if user has no money to player anymore and will end the game

            //FIRST WHILE LOOP TO KEEP THE GAME RUNNING AFTER EACH PLAY
            while (true)
            {
                //ALONG THE GAME THE SYSTEM WILL CHECK IF USER HAS ENOUGH MONEY TO PLAY
                while (!isPlayerMoneyZero) 
                {
                    //BET MIN 1$ max 3$
                    Console.WriteLine("Please bet min 1 or max 3 dollars to spin");

                    int.TryParse(Console.ReadLine(), out int playerBet); //reading end user input and parsing it to an int

                    //BASED ON  BET DEFINE ARRAYS AND STORE RANDOM VALUES, player bet defines the first dimensional array

                    int[,] slot = new int[playerBet, MAX_SPIN_VALUE];

                    for (int i = 0; i < slot.GetLength(0); i++)
                    {
                        for (int j = 0; j < slot.GetLength(1); j++)
                        {
                            int randomNr = random.Next(MAX_SPIN_VALUE);
                            slot[i, j] = randomNr;
                            Console.Write($"{randomNr} ");
                        }
                        Console.WriteLine();
                    }

                }
            }

         
        }
    }
}