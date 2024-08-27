using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slot_Machine
{
    public class Constants
    {
        //Defining winning line possibilities
        public const string HORIZONTAL_CHOICE = "Horizaontal";
        public const string VERTICAL_CHOICE = "Vertical";
        public const string DIAGONAL_CHOICE = "Diagonal";

        public const int START_MONEY = 100; //virtual money every gamer starts with 
        public const int MINIMUM_BET = 3; //Minimum bet

        public const int DIFFICULTY = 2; //Which is easy. random will generate nr in grid between 0 and 2

        //Defining Grid ROW and COL Size
        public const int GRID_ROW = 3;
        public const int GRID_COL = 3;


        public const int GAIN = 1;//Total dollar per winning line
    }
}

