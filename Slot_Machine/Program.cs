using System.Data.SqlTypes;

namespace Slot_Machine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //instantiating Random class to get random numbers for the slot machine
            Random random = new Random();
           
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

                    //BASED ON  BET DEFINE ARRAYS

                
                }
            }

         
        }
    }
}