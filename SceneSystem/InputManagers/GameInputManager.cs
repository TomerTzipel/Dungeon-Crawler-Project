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
  
                case ConsoleKey.D1:
                case ConsoleKey.D2:
                case ConsoleKey.D3:
                case ConsoleKey.D4:
                case ConsoleKey.D5:
                case ConsoleKey.D6:
                case ConsoleKey.D7:
                case ConsoleKey.D8:
                case ConsoleKey.D9:
                case ConsoleKey.Backspace:
                    return InputType.Sudoku;

                case ConsoleKey.I:
                case ConsoleKey.Escape:
                    return InputType.SceneChange;
                    

                default:
                    return InputType.Error;
                    
            }
        }
        public Direction TranslateMovementInput()
        {
            switch (LastInput.Key)
            {
                case ConsoleKey.UpArrow:
                    return Direction.Up;

                case ConsoleKey.DownArrow:
                    return Direction.Down;

                case ConsoleKey.LeftArrow:
                    return Direction.Left;

                case ConsoleKey.RightArrow:
                    return Direction.Right;

                default:
                    return Direction.Error;
            }
        }
        public int TranslateSudokuInput()
        {
            switch (LastInput.Key)
            {
                case ConsoleKey.D1:
                    return 1;

                case ConsoleKey.D2:
                    return 2;

                case ConsoleKey.D3:
                    return 3;

                case ConsoleKey.D4:
                    return 4;

                case ConsoleKey.D5:
                    return 5;

                case ConsoleKey.D6:
                    return 6;

                case ConsoleKey.D7:
                    return 7;

                case ConsoleKey.D8:
                    return 8;

                case ConsoleKey.D9:
                    return 9;

                case ConsoleKey.Backspace:
                    if (LastInput.Modifiers.HasFlag(ConsoleModifiers.Control)) return -1;

                    return 0;

                default: return -1;
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
