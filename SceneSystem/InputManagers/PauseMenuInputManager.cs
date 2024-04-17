using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler.SceneSystem.InputManagers
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
