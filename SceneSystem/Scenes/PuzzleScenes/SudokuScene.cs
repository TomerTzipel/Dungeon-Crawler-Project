


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
                base.SceneLoop();
            }

        }

        protected override void HandleInput()
        {
            ConsoleKeyInfo input = _inputManager.LastInput;
            InputType inputType = _inputManager.LastInputType;

            switch (inputType)
            {
                case InputType.Movement:
                    HandleMovement();
                    break;

                case InputType.Sudoku:
                    HandleSudoku();
                    break;

                case InputType.SudokuEdit:
                    HandleSudokuEdit(input);
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
            bool wasSolved = puzzle.InputValueAtSolver(value, true);
            if (wasSolved)
            {
                Printer.AddActionText(ActionTextType.Item,"A reward for solving the sudoku:");
                LootManager.RewardTrinket();
                level.DeActivatePuzzle();
                return;
            }

            Printer.PrintLevel();
            InputManager.CleanInputBuffer();
        }
        private void HandleSudokuEdit(ConsoleKeyInfo input)
        {
            ConsoleKey inputKey = input.Key;
            Level level = LevelManager.CurrentLevel;
            SudokuPuzzle puzzle = (SudokuPuzzle)level.Puzzle;

            switch (inputKey)
            {
                case ConsoleKey.Z:
                    if (input.Modifiers.HasFlag(ConsoleModifiers.Control)) 
                    {
                        puzzle.UndoAction();
                    }
                    break;

                case ConsoleKey.Y:
                    if (input.Modifiers.HasFlag(ConsoleModifiers.Control))
                    {
                        puzzle.RedoAction();
                    }
                    break;
            }

            SceneManager.PrintCurrentScene();
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
           
            LevelManager.CurrentLevel.PrintLevel();
            Printer.ColorReset();
        }
    }
}

