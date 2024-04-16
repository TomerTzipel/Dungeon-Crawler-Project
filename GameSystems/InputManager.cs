

namespace GameSystems
{

    public enum InputType
    {
        Movement, Menu, Sudoku, Error
    }

    public static class InputManager
    {
        private static ConsoleKeyInfo _lastInput;
        private static ConsoleKey _lastInputKey;
        private static InputType _lastInputType;

        public static InputType ReadInput()
        {
            if (Console.KeyAvailable)
            {
                _lastInput = Console.ReadKey(true);
                _lastInputKey = _lastInput.Key;
                TranslateInput();

                return _lastInputType;
            }

            return InputType.Error;
        }


        public static void HandleMovementInput(Level level)
        {
            Direction direction = TranslateMovementInput();
            level.MovePlayer(PlayerManager.PlayerElement,direction);
            FinishedHandlingInput(level);
        }
        public static void HandleSudokuInput(Level level)
        {
            if (level.IsPuzzleActive && level.Puzzle is SudokuPuzzle puzzle)
            {
                int value = TranslateSudokuInput();
                if (value < 0) 
                {
                    level.DeActivatePuzzle();
                    return;
                }

                bool wasSolved = puzzle.InputValue(value);
                if (wasSolved)
                {
                    //generate reward
                    level.DeActivatePuzzle();
                    return;
                }
            }

            FinishedHandlingInput(level);
        }

        private static void FinishedHandlingInput(Level level)
        {
            Printer.PrintLevel();
            CleanInputBuffer();
        }

        private static void TranslateInput()
        {
            switch (_lastInputKey)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.DownArrow:
                case ConsoleKey.LeftArrow:
                case ConsoleKey.RightArrow:
                    _lastInputType = InputType.Movement;
                    break;

                case ConsoleKey.D1:
                case ConsoleKey.D2:
                case ConsoleKey.D3:
                case ConsoleKey.D4:
                case ConsoleKey.D5:
                case ConsoleKey.D6:
                case ConsoleKey.D7:
                case ConsoleKey.D8:
                case ConsoleKey.D9:
                case ConsoleKey.Backspace:
                    _lastInputType = InputType.Sudoku;
                    break;

                default:
                    _lastInputType = InputType.Error;
                    break;
            }
        }

        private static Direction TranslateMovementInput()
        {
            switch (_lastInputKey)
            {
                case ConsoleKey.UpArrow:
                    return Direction.Up;

                case ConsoleKey.DownArrow:
                    return Direction.Down;

                case ConsoleKey.LeftArrow:
                    return Direction.Left;

                case ConsoleKey.RightArrow:
                    return Direction.Right;

                default:
                    return Direction.Error;
            }
        }
        private static int TranslateSudokuInput()
        {
            switch (_lastInputKey)
            {
                case ConsoleKey.D1:
                    return 1;

                case ConsoleKey.D2:
                    return 2;

                case ConsoleKey.D3:
                    return 3;

                case ConsoleKey.D4:
                    return 4;

                case ConsoleKey.D5:
                    return 5;

                case ConsoleKey.D6:
                    return 6;

                case ConsoleKey.D7:
                    return 7;

                case ConsoleKey.D8:
                    return 8;

                case ConsoleKey.D9:
                    return 9;

                case ConsoleKey.Backspace:
                    if (_lastInput.Modifiers.HasFlag(ConsoleModifiers.Control)) return -1;

                    return 0;

                default: return -1;  
            }
        }
        private static void CleanInputBuffer()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
        }
    }
}
