

using GameSystems;

namespace SceneSystem
{
    public class ChessScene : Scene
    {
        private bool _isCurrentScene;

        public ChessScene() : base(new ChessInputManager()) { }

        protected ChessInputManager SceneInputManager
        {
            get
            {
                return (ChessInputManager)_inputManager;
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

            while (LevelManager.CurrentLevel.IsPuzzleActive && _isCurrentScene)
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

                case InputType.Chess:
                    HandleChess(input);
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

        private void HandleChess(ConsoleKeyInfo input)
        {
            Level level = LevelManager.CurrentLevel;
            ChessPuzzle puzzle = (ChessPuzzle)level.Puzzle;

            switch (input.Key)
            {
                case ConsoleKey.Spacebar:
                case ConsoleKey.Enter:
                    if (puzzle.IsHoldingPiece)
                    {
                        bool result = puzzle.PlacePiece();
                        FinishPuzzle(result);
                        return;
                    }
                    else
                    {
                        puzzle.LiftPiece();
                    }
                    break;

                case ConsoleKey.Backspace:
                    if (input.Modifiers.HasFlag(ConsoleModifiers.Control))
                    {
                        level.DeActivatePuzzle();
                        return;
                    }
                    break;
            }
            Printer.PrintLevel();
            InputManager.CleanInputBuffer();
        }

        private void FinishPuzzle(bool result)
        {
            Level level = LevelManager.CurrentLevel;

            if (result)
            {
                AudioManager.Play(AudioType.PuzzleSolved);
                Printer.AddActionText(ActionTextType.Item, "A reward for finding the checkmate:");
                LootManager.RewardTrinket();
                level.DeActivatePuzzle();
            }
            else
            {
                ChessPuzzle puzzle = (ChessPuzzle)level.Puzzle;
                puzzle.ResetPuzzle();
            }
        }

        private void HandleSceneChange()
        {
            SceneType scene = SceneInputManager.TranslateSceneChangeInput();
            SceneManager.ChangeScene(scene);

            _isCurrentScene = false;
        }

        public override void PrintScene()
        {
            LevelManager.CurrentLevel.PrintLevel();
            Printer.ColorReset();
        }
    }
}

