
namespace SceneSystem
{
    public enum InputType
    {
        Movement, MenuMovement, Sudoku, SceneChange,ButtonClick ,Error
    }
    public abstract class InputManager
    {
        public ConsoleKeyInfo LastInput { get; protected set; }
        public InputType LastInputType { get; protected set; }

        public bool ReadInput()
        {
            if (Console.KeyAvailable)
            {
                LastInput = Console.ReadKey(true);
                LastInputType = TranslateInput();

                return true;
            }

            return false; 
        }

        protected abstract InputType TranslateInput();

        public static void CleanInputBuffer()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
        }

    }
}
