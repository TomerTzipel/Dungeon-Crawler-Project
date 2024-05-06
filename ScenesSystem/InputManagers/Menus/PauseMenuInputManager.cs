

namespace SceneSystem
{
    public class PauseMenuInputManager : SelectionMenuInputManager
    {
        protected override InputType TranslateInput()
        {
            if (LastInput.Key == ConsoleKey.Escape) return InputType.SceneChange;

            return base.TranslateInput();
        }
    }
}
