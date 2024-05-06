

namespace SceneSystem
{
    public class SelectionMenuInputManager : InputManager
    {
        protected override InputType TranslateInput()
        {
            switch (LastInput.Key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.DownArrow:
                case ConsoleKey.W:
                case ConsoleKey.S:
                    return InputType.MenuMovement;

                case ConsoleKey.Enter:
                case ConsoleKey.Spacebar:
                    return InputType.ButtonClick;

                default:
                    return InputType.Error;
            }
        }

      

   




    }
}
