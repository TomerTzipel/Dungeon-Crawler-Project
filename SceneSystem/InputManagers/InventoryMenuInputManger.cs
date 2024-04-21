using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneSystem
{
    public class InventoryMenuInputManger : InputManager
    {
        protected override InputType TranslateInput()
        {
            switch (LastInput.Key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.DownArrow:
                case ConsoleKey.LeftArrow:
                case ConsoleKey.RightArrow:
                case ConsoleKey.W:
                case ConsoleKey.A:
                case ConsoleKey.S:
                case ConsoleKey.D:
                    return InputType.MenuMovement;

                case ConsoleKey.I:
                case ConsoleKey.Escape:
                    return InputType.SceneChange;

                case ConsoleKey.Enter:
                case ConsoleKey.Spacebar:
                    return InputType.ButtonClick;

                default:
                    return InputType.Error;

            }
        }
        public Direction TranslateMenuMovementInput()
        {
            switch (LastInput.Key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    return Direction.Up;

                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    return Direction.Down;

                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    return Direction.Left;

                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    return Direction.Right;

                default:
                    return Direction.Error;
            }
        }

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
