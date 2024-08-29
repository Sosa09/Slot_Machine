using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Slot_Machine
{
    public static class Logic
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="random"></param>
        /// <param name="TotalGrid"></param>
        /// <returns></returns>
        public static int[] GenerateSlotNumbers(Random random, int TotalGrid)
        {
            int[] randomNumbers = new int[TotalGrid];
            for (int i = 0; i < randomNumbers.Length; i++)
            {
                randomNumbers[i] = random.Next(Constants.DIFFICULTY);
            }
            return randomNumbers;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static int GetHorizontalTotalWinningAreas(int[,] grid) 
        {
            int winnerAreaCount = 0;
            for (int i = 0; i < Constants.GRID_ROW; i++)
            {
                bool winner = true; //reset winner
                int firstValue = grid[i, 0];

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
                    winnerAreaCount++;
                }
            }
            return winnerAreaCount;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static int GetVerticalTotalWinningAreas(int[,] grid)
        {
            int winnerAreaCount = 0;
            //looping trough each colomn
            for (int i = 0; i < Constants.GRID_COL; i++)
            {
                //storing the first element of each column(0, i);

                bool winner = true;
                int firstValue = grid[0, i];
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
                    winnerAreaCount++;
                }
            }
            return winnerAreaCount;
        }
        /// <summary>
        /// TODO: refactor this approach
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static int GetDiagonalTotalWinningAreas(int[,] grid)
        {
            int winnerAreaCount = 0;
            int firstValue = grid[grid.GetLowerBound(0), grid.GetLowerBound(1)];
            int lastValue = grid[grid.GetLowerBound(0), grid.GetUpperBound(0)];
            bool winner = true;

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
                winnerAreaCount++;
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
                winnerAreaCount++;
            }
            return winnerAreaCount;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="winnerLineCount"></param>
        /// <param name="playerBet"></param>
        /// <param name="profit"></param>
        /// <param name="wallet"></param>
        public static void UpdateGamerWallet(int winnerLineCount, int playerBet,ref int profit, ref int wallet)
        {
            if (winnerLineCount > 0)
            {
                profit += winnerLineCount;
                wallet += winnerLineCount;
            }
            else
            {
                profit -= winnerLineCount;
                wallet -= playerBet;
            }
        }

        public static bool GamerHasMoney(int wallet, bool gamerWalletNotEmpty)
        {
            if (wallet < Constants.MINIMUM_BET)
                return !gamerWalletNotEmpty;
            return gamerWalletNotEmpty;
        }
    }
}
