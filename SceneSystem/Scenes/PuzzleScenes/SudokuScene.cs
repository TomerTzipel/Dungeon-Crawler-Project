

namespace SceneSystem
{
    public class SudokuScene : Scene
    {

        private bool _isCurrentScene;

        public SudokuScene() : base(new SudokuInputManager()) { }

        protected SudokuInputManager SceneInputManager
        {
            get
            {
                return (SudokuInputManager)_inputManager;
            }
        }

        protected override void EnterScene()
        {
            _isCurrentScene = true;
            SceneManager.PrintCurrentScene();
        }
        public override void SceneLoop()
        {
            EnterScene();

            while(LevelManager.CurrentLevel.IsPuzzleActive && _isCurrentScene)
            {
                bool wasInputDetected = _inputManager.ReadInput();

                if (wasInputDetected)
                {
                    HandleInput();
                }
            }

        }

        protected override void HandleInput()
        {
            InputType inputType = _inputManager.LastInputType;

            switch (inputType)
            {
                case InputType.Movement:
                    HandleMovement();
                    break;

                case InputType.Sudoku:
                    HandleSudoku();
                    break;

                case InputType.SceneChange:
                    HandleSceneChange();
                    break;
                default:
                    break;
            }
        }
        private void HandleMovement()
        {
            Direction direction = SceneInputManager.TranslateMovementInput();
            LevelManager.CurrentLevel.MoveSolver(direction);
            Printer.PrintLevel();
            InputManager.CleanInputBuffer();
        }
        private void HandleSudoku()
        {
            Level level = LevelManager.CurrentLevel;

            int value = SceneInputManager.TranslateSudokuInput();
            if (value < 0)
            {
                level.DeActivatePuzzle();
                return;
            }

            SudokuPuzzle puzzle = (SudokuPuzzle)level.Puzzle;
            bool wasSolved = puzzle.InputValue(value);
            if (wasSolved)
            {
                //generate reward for player
                level.DeActivatePuzzle();
                return;
            }

            Printer.PrintLevel();
            InputManager.CleanInputBuffer();
        }

        private void HandleSceneChange()
        {
            SceneType scene = SceneInputManager.TranslateSceneChangeInput();
            SceneManager.ChangeScene(scene);

            _isCurrentScene = false;

            InputManager.CleanInputBuffer();
        }

        public override void PrintScene()
        {
            Console.SetCursorPosition(0, 0);
            LevelManager.CurrentLevel.PrintLevel();
            Printer.ColorReset();
        }
    }
}

