




namespace GameSystems
{

   

    public static class GameInputManagerOld
    {
        private static ConsoleKeyInfo _lastInput;
        private static ConsoleKey _lastInputKey;
        private static InputType _lastInputType = InputType.MenuMovement;

        public static InputType ReadInput()
        {
            if (Console.KeyAvailable)
            {
                _lastInput = Console.ReadKey(true);
                _lastInputKey = _lastInput.Key;
                TranslateInput();

                return _lastInputType;
            }

            return InputType.Error;
        }

        public static void HandleMovementInput(Level level)
        {
            Direction direction = TranslateMovementInput();
            level.MovePlayer(PlayerManager.PlayerElement,direction);
            FinishedHandlingInput(level);
        }
        public static void HandleSudokuInput(Level level)
        {
            if (level.IsPuzzleActive && level.Puzzle is SudokuPuzzle puzzle)
            {
                int value = TranslateSudokuInput();
                if (value < 0) 
                {
                    level.DeActivatePuzzle();
                    return;
                }

                bool wasSolved = puzzle.InputValue(value);
                if (wasSolved)
                {
                    //generate reward
                    level.DeActivatePuzzle();
                    return;
                }
            }

            FinishedHandlingInput(level);
        }

        public static void HandleSceneChnageInput()
        {

            SceneType scene = TranslateSceneChangeInput();

            if(scene == SceneType.ExitGame)
            {
                return;
            }

            //Printer.Scene = scene;
            Printer.PrintScene(scene);
            //Enter Inventory Menu Loop
        }

      

        private static void FinishedHandlingInput(Level level)
        {
            Printer.PrintLevel();
            CleanInputBuffer();
        }

        private static void TranslateInput()
        {
            /*switch (SceneManager.Scene)
            {
                case SceneType.MainMenu:
                    switch (_lastInputKey)
                    {
                        case ConsoleKey.UpArrow:
                        case ConsoleKey.DownArrow:
                            _lastInputType = InputType.MenuMovement;
                            break;

                        default:
                            _lastInputType = InputType.Error;
                            break;
                    }
                break;


                case SceneType.Game:
                    switch (_lastInputKey)
                    {
                        case ConsoleKey.UpArrow:
                        case ConsoleKey.DownArrow:
                        case ConsoleKey.LeftArrow:
                        case ConsoleKey.RightArrow:
                            _lastInputType = InputType.Movement;
                            break;

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
                            _lastInputType = InputType.Sudoku;
                            break;

                        case ConsoleKey.I:
                        case ConsoleKey.Escape:
                            _lastInputType = InputType.SceneChange;
                            break;

                        default:
                            _lastInputType = InputType.Error;
                            break;
                    }
                    break;




                case SceneType.Inventory:
                    switch (_lastInputKey)
                    {
                        case ConsoleKey.I:
                        case ConsoleKey.Escape:
                            _lastInputType = InputType.SceneChange;
                            break;
                    }
                    break;



                default:
                    _lastInputType = InputType.Error;
                    break;
            }




*/


        }
        private static SceneType TranslateSceneChangeInput()
        {
            /*switch (_lastInputKey)
            {
                case ConsoleKey.I:
                    if(Printer.Scene == SceneType.Inventory) return SceneType.Game;
                    return SceneType.Inventory;

                case ConsoleKey.Escape:
                    if (Printer.Scene == SceneType.Inventory) return SceneType.Game;
                    return SceneType.ExitGame;

                default:
                    return SceneType.ExitGame;
            }*/

            return SceneType.Game;

        }

        private static Direction TranslateMovementInput()
        {
            switch (_lastInputKey)
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
        private static int TranslateSudokuInput()
        {
            switch (_lastInputKey)
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
                    if (_lastInput.Modifiers.HasFlag(ConsoleModifiers.Control)) return -1;

                    return 0;

                default: return -1;  
            }
        }
        private static void CleanInputBuffer()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
        }

        public static void HandleMenuInput()
        {
            /*switch (Printer.Scene)
            {
                case SceneType.MainMenu:
                    switch (_lastInputKey)
                    {
                        case ConsoleKey.UpArrow:
                            MainMenu.PriorButton();
                            break;

                        case ConsoleKey.DownArrow:
                            MainMenu.NextButton();
                            break;

                        case ConsoleKey.Spacebar:
                            MainMenu.ActivateSelectedButton();
                            break;

                    }
                    break;

                case SceneType.Inventory:

                    break;

            }*/

        }
    }
}
