

using System.Reflection.Emit;
using Utility;

namespace GameSystems
{
    public class Level
    {
        private Camera _camera;
        public Map Map { get; private set; }
        
        public Puzzle Puzzle { get; private set; }

        public bool IsPuzzleActive { get; private set; } = false;

        public Level(int matrixSectionSize, int numberOfSections,MapComposition mapComposition, PlayerElement player)
        {
            GenerateMapLayout(numberOfSections, matrixSectionSize, mapComposition, player);
            _camera = new Camera(21,13);
        }

        private void GenerateMapLayout(int numberOfSectionsToGenerate, int matrixSectionSize, MapComposition mapComposition, PlayerElement player)
        {
            Map = new Map(numberOfSectionsToGenerate, matrixSectionSize, mapComposition,player);
           
        }

        public void PrintMiniMap()
        {
            Console.WriteLine();
            Map.PrintMiniMap();
        }

        public void PrintLevel()
        {
            if (IsPuzzleActive)
            {
                PrintPuzzle();
            }
            else
            {
                PrintCamera();
            }
        }

        public void PrintCamera()
        {
            Console.WriteLine();
            Map.PrintToCamera(_camera);
        }

        public void MovePlayer(PlayerElement player,Direction direction)
        {
            if (IsPuzzleActive)
            {
                Puzzle.MoveSolverInDirection(direction);
                return;
            }

            Map.MoveElementInDirection(player, direction);
        }

        public void ActivatePuzzle(Puzzle puzzle) 
        {
            Puzzle = puzzle;
            IsPuzzleActive = true;
        }

        public void DeActivatePuzzle()
        {
            Puzzle = null;
            IsPuzzleActive = false;
            Printer.PrintGameScene(this);
            GameManager.ResumeGame();

        }
        public void PrintPuzzle()
        {
            Console.WriteLine();
            Puzzle.PrintPuzzle();
        }
    }
}
