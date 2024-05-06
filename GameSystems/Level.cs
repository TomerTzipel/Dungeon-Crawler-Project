

using SceneSystem;
using System.Reflection.Emit;
using Utility;

namespace GameSystems
{
    public class Level
    {
        
        private MiniMap _miniMap;

        public Map Map { get; protected set; }
        
        public Puzzle Puzzle { get; private set; }
        public PuzzleType PuzzleType { get; private set; }
        public bool IsPuzzleActive { get; private set; } = false;

       
        protected Level() { }
 
        public Level(int matrixSectionSize, int numberOfSections,MapComposition mapComposition, PlayerElement player)
        {
            GenerateMapLayout(numberOfSections, matrixSectionSize, mapComposition, player);
            GenerateMiniMap(Map.SectionsMatrix, matrixSectionSize);
        }

        private void GenerateMapLayout(int numberOfSectionsToGenerate, int matrixSectionSize, MapComposition mapComposition, PlayerElement player)
        {
            Map = new Map(numberOfSectionsToGenerate, matrixSectionSize, mapComposition,player);
           
        }
        protected void GenerateMiniMap(SectionMatrix sectionMatrix , int size)
        {
            _miniMap = new MiniMap(sectionMatrix, size);
        }


        public void PrintLevel()
        {
            if (IsPuzzleActive)
            {
                PrintPuzzle();
                return;
            }

            PrintCameraAndMiniMap();
        }

        public void PrintCameraAndMiniMap()
        {
            Console.SetCursorPosition(0, 5);
            Map.PrintToCamera(Printer.Camera);

            Printer.ColorReset();

            if (_miniMap.DoesRequireReprint)
            {
                PrintMiniMap();
                _miniMap.DoesRequireReprint = false;
            }


        }

        public void PrintPuzzle()
        {
            Puzzle.PrintPuzzle();
        }
        public void PrintMiniMap()
        {
            _miniMap.Print();
        }

        public void PrintHUD()
        {
            PlayerManager.CombatEntity.PrintPlayerStatus();
        }

        public void MovePlayer(PlayerElement player,Direction direction)
        {
            Map.MoveElementInDirection(player, direction);
            _miniMap.DiscoverSectionsAroundPlayer();
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
