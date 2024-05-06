
namespace SceneSystem
{
    public class InventoryInputManger : UserInterfaceInputManager
    {
        public SceneType TranslateSceneChangeInput()
        {
            switch (LastInput.Key)
            {
                case ConsoleKey.I:
                    return SceneType.Game;

                case ConsoleKey.Escape:
                    return SceneType.PauseMenu;

                default:
                    return SceneType.Game;

            }
        }


    }
}
