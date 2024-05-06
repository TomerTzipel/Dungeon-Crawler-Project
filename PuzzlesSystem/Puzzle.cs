
namespace PuzzleSystem
{
    public enum PuzzleType
    {
        Sudoku, Chess, BreakingFloor
    }

    public class Puzzle : Map
    {
        protected PuzzleSolver _solver = new PuzzleSolver();

        public void PrintPuzzle()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Element elementToRender = ElementAt(i, j);
                    Printer.CheckColors(elementToRender);
                    Console.Write(elementToRender);
                }
                Console.WriteLine();
            }
        }

        public void MoveSolverInDirection(Direction direction)
        {
           MoveElementInDirection(_solver, direction);
           
        }

       
        

    }
}


