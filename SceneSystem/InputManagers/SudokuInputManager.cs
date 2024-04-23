

namespace SceneSystem
{
    public class SudokuInputManager : GameInputManager
    {
        protected override InputType TranslateInput()
        {
            switch (LastInput.Key)
            {
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
                    return InputType.Sudoku;

                case ConsoleKey.Z:
                case ConsoleKey.Y:
                    return InputType.SudokuEdit;
            }

            return base.TranslateInput();
        }

        public int TranslateSudokuInput()
        {
            switch (LastInput.Key)
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
                    if (LastInput.Modifiers.HasFlag(ConsoleModifiers.Control)) return -1;
                    return 0;

                default: return -1;
            }
        }
    }
}
