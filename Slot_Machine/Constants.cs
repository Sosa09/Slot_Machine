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
        public const char   HORIZONTAL  = '0';
        public const char   VERTICAL = '1';
        public const char   DIAGONAL = '2';
        public const int    MAX_PLAY_DIRECTIONS = 3;
        public const int    START_MONEY = 100; //virtual money every gamer starts with 
        public const int    MINIMUM_BET = 3; //Minimum bet
        public const int    DIFFICULTY  = 2; //Which is easy. random will generate nr in grid between 0 and 2
        //Defining Grid ROW and COL Size
        public const int    GRID_ROW    = 3;
        public const int    GRID_COL    = 3;
        public const int    GAIN        = 1;//Total dollar per winning line
        public static bool  BETVALID    = true;
        public static bool  BETNOTVALID = false;
    }
}

