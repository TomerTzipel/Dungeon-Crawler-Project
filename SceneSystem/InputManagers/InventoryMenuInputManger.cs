using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneSystem
{
    public class InventoryMenuInputManger : UserInterfaceInputManager
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
