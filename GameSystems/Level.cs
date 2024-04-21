

using SceneSystem;
using System.Reflection.Emit;
using Utility;

namespace GameSystems
{
    public class Level
    {
        private Camera _camera;
        public Map Map { get; private set; }
        
        public Puzzle Puzzle { get; private set; }
        public PuzzleType PuzzleType { get; private set; }
        public bool IsPuzzleActive { get; private set; } = false;

        public Level(int matrixSectionSize, int numberOfSections,MapComposition mapComposition, PlayerElement player)
        {
            GenerateMapLayout(numberOfSections, matrixSectionSize, mapComposition, player);
            _camera = new Camera(Printer.CAMERA_WIDTH, Printer.CAMERA_HEIGHT);
        }

        private void GenerateMapLayout(int numberOfSectionsToGenerate, int matrixSectionSize, MapComposition mapComposition, PlayerElement player)
        {
            Map = new Map(numberOfSectionsToGenerate, matrixSectionSize, mapComposition,player);
           
        }

       

        public void PrintLevel()
        {
            if (IsPuzzleActive)
            {
                PrintPuzzle();
                return;
            }

            PrintCamera();
        }

        public void PrintCamera()
        {
            //Printer.SetPrinterPosition(0, 5);
            Console.SetCursorPosition(0, 5);
            Map.PrintToCamera(_camera);
        }

        public void PrintPuzzle()
        {
            Puzzle.PrintPuzzle();
        }
        public void PrintMiniMap()
        {
            Map.PrintMiniMap();
        }

        public void PrintHUD()
        {
            PlayerManager.PlayerElement.CombatEntity.PrintPlayerStatus();
        }


        public void MovePlayer(PlayerElement player,Direction direction)
        {
            Map.MoveElementInDirection(player, direction);
        }

        public void MoveSolver(Direction direction)
        {
            Puzzle.MoveSolverInDirection(direction); 
        }

        public void ActivatePuzzle(Puzzle puzzle,PuzzleType type) 
        {
            Puzzle = puzzle;
            PuzzleType = type;
            IsPuzzleActive = true;
            GameManager.PauseGame();
            SceneManager.ChangeScene(SceneType.Puzzle);
        }

        public void DeActivatePuzzle()
        {
            Puzzle = null;
            IsPuzzleActive = false;
            SceneManager.ChangeScene(SceneType.Game);
        }
      
    }
}
