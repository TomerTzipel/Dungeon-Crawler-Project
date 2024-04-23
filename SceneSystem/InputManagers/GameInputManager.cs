using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneSystem
{
    public class GameInputManager : InputManager
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
                    return InputType.Movement;
  
                case ConsoleKey.I:
                case ConsoleKey.Escape:
                    return InputType.SceneChange;

                case ConsoleKey.Q:
                    return InputType.Potion;


                default:
                    return InputType.Error;
                    
            }
        }
        public Direction TranslateMovementInput()
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
                    return SceneType.Inventory;

                case ConsoleKey.Escape:
                    return SceneType.PauseMenu;

                default: 
                    return SceneType.Game;
               
            }
        }

    }
}
