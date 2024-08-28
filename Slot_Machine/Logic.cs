using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slot_Machine
{
    public static class Logic
    {
        public static int[] GenerateSlotNumbers(Random random, int TotalGrid)
        {
            int[] randomNumbers = new int[TotalGrid];
            for (int i = 0; i < randomNumbers.Length; i++)
            {
                randomNumbers[i] = random.Next(Constants.DIFFICULTY);
            }
            return randomNumbers;
        }
    }
}
