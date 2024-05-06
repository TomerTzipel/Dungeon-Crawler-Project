
namespace SceneSystem
{
    public class ChessInputManager : GameInputManager
    {
        protected override InputType TranslateInput()
        {
            switch (LastInput.Key)
            {
                case ConsoleKey.Spacebar:
                case ConsoleKey.Enter:
                case ConsoleKey.Backspace:
                    return InputType.Chess;
            }

            return base.TranslateInput();
        }
    }
}
