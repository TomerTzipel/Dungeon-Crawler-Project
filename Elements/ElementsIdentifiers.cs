
namespace Elements
{
    public static class ElementsIdentifiers
    {
        /* Legend:
         * EBC - Element Background Color
         * EFC - Element Foregorund Color
         * EI - Element Identifier
         */

        //General Colors:
        public const ConsoleColor DEFAULT_EBC = ConsoleColor.Black;
        public const ConsoleColor DEFAULT_EFC = ConsoleColor.White;
        public const ConsoleColor BLOOD_PUDDLE_EBC = ConsoleColor.DarkRed;
        public const ConsoleColor QUICKSAND_EBC = ConsoleColor.DarkYellow;

        //Empty:
        public const string EMPTY_EI = "  ";
        public const ConsoleColor OUTER_EMPTY_EBC = ConsoleColor.DarkBlue;
        
        //Obstacles:
        public const string VERTICAL_WALL_EI = "||";
        public const string HORIZONTAL_WALL_EI = "==";

        public const string DOOR_EI = "HH";

        public const string SPAWNER_EI = "SP";

        public const string CHEST_EI = "CH"; public const ConsoleColor CHEST_EFC = ConsoleColor.Yellow;

        //Player Elements:
        public const string PLAYER_RIGHT_EI = "I/";
        public const string PLAYER_LEFT_EI = "\\I";

        //Enemy Elements:
        public const string BAT_DDWN_EI = "/\\";
        public const string BAT_UP_EI = "\\/";

        public const string OGRE_EI = "GR";

        public const string SLIME3_EI = "O ";
        public const string SLIME2_EI = "o ";
        public const string SLIME1_EI = ". ";
        //Walkables:
        public const string EXIT_EI = "EX";
        public const string ENTRY_EI = "ST";
        public const string PUZZLE_EI = "PZ";
        public const string TRAP_EI = "TR"; 
       

        //Puzzles related:
        public const string PUZZLE_SOLVER_EI = "I"; public const ConsoleColor PUZZLE_SOLVER_EFC = ConsoleColor.Yellow;
        public const ConsoleColor GIVEN_NUMBER_EFC = ConsoleColor.Green;
        public const ConsoleColor MISSING_NUMBER_EFC = ConsoleColor.White;
        public const ConsoleColor ERRONEOUS_EBC = ConsoleColor.DarkRed;

        //Decorations & Rewardables
        public const string BOULDER_EI = "* "; public const ConsoleColor BOULDER_EFC = ConsoleColor.DarkGray;
        public const string VASE_EI = "db"; public const ConsoleColor VASE_EFC = ConsoleColor.DarkMagenta;

        public const string GRASS_EI = ",,"; public const ConsoleColor GRASS_EFC = ConsoleColor.Green;
        public const string PEBBLE_EI = ". "; public const ConsoleColor PEBBLE_EFC = ConsoleColor.Gray;
        public const string BUSH_EI = "qp"; public const ConsoleColor BUSH_EFC = ConsoleColor.Green;
        public const string TREE_EI = "T "; public const ConsoleColor TREE_EFC = ConsoleColor.DarkGreen;
    }
}
