

namespace SceneSystem
{
    public class ShopInputManager : UserInterfaceInputManager
    {
        public SceneType TranslateSceneChangeInput()
        {
            switch (LastInput.Key)
            {
                case ConsoleKey.I:
                    return SceneType.Inventory;

                case ConsoleKey.Escape:
                    return SceneType.PauseMenu;

                default:
                    return SceneType.Game;

            }
        }
    }
}
