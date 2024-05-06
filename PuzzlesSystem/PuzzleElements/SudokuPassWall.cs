

namespace PuzzleSystem
{
    public class SudokuPassWall : WalkableElement
    {
        private static SudokuPassWall _verticalWallInstance;
        private static SudokuPassWall _horizontalWallInstance;

        private static bool _areInstancesInit = false;

        protected SudokuPassWall(string identifer) : base(identifer) { }

        public static SudokuPassWall VerticalWallInstance
        {
            get
            {
                if (!_areInstancesInit)
                {
                    InitializeObstacles();
                }

                return _verticalWallInstance;
            }
        }
        public static SudokuPassWall HorizontalWallInstance
        {
            get
            {
                if (!_areInstancesInit)
                {
                    InitializeObstacles();
                }

                return _horizontalWallInstance;
            }
        }

        private static void InitializeObstacles()
        {
            if (!_areInstancesInit)
            {
                _verticalWallInstance = new SudokuPassWall(VERTICAL_WALL_EI);
                _horizontalWallInstance = new SudokuPassWall(HORIZONTAL_WALL_EI);

                _areInstancesInit = true;
            }
        }
    }
}
