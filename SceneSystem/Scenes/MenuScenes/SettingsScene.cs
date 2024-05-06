

namespace SceneSystem
{
    public class SettingsScene : SelectionMenuScene
    {
        protected UserInterfaceInputManager SceneInputManager
        {
            get
            {
                return (UserInterfaceInputManager)_inputManager;
            }
        }

        public SettingsScene() : base(14, "Settings", ConsoleColor.Yellow)
        {
            _inputManager = new UserInterfaceInputManager();
            SetUpSettingsScene();
        }

        public void SetUpSettingsScene()
        {
            
            _buttons[0] = new DifficultyButton(Difficulty.Dynamic);
            _buttons[1] = new DifficultyButton(Difficulty.Easy);
            _buttons[2] = new DifficultyButton(Difficulty.Normal);
            _buttons[3] = new DifficultyButton(Difficulty.Hard);
            

            _buttons[4] = new PlayerColorButton(ConsoleColor.White);
            _buttons[5] = new PlayerColorButton(ConsoleColor.Blue);
            _buttons[6] = new PlayerColorButton(ConsoleColor.Green);
            _buttons[7] = new PlayerColorButton(ConsoleColor.Yellow);
            _buttons[8] = new PlayerColorButton(ConsoleColor.Magenta);
            _buttons[9] = new PlayerColorButton(ConsoleColor.Cyan);

            _buttons[10] = new RevealMiniMapButton();

            _buttons[11] = new InvincibilityButton();

            _buttons[12] = new ExitPrerequisiteButton();

            _buttons[13] = new ChangeSceneButton("Main Menu", SceneType.MainMenu);

            _buttons[0].OnClick();
            _buttons[4].OnClick();
            _buttons[0].Select();

            _selectedButtonIndex = 0;

        }

        protected override void EnterScene()
        {
            _buttons[_selectedButtonIndex].Select();
            _wasButtonClicked = false;
            SceneManager.PrintCurrentScene();
        }

        public override void SceneLoop()
        {
            EnterScene();

            while (!_wasButtonClicked)
            {
                base.SceneLoop();
            }
        }
        protected override void HandleInput()
        {
            InputType inputType = _inputManager.LastInputType;

            switch (inputType)
            {
                case InputType.MenuMovement:
                    HandleMenuMovement();
                    break;

                case InputType.ButtonClick:
                    HandleButtonClick();
                    break;

                default:
                    break;
            }
        }

        private void HandleButtonClick()
        {
            ActivateButton(_buttons[_selectedButtonIndex]);

            ToggleButton currentButton;
            if (_selectedButtonIndex < 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    if(i != _selectedButtonIndex)
                    {
                        currentButton = (ToggleButton)_buttons[i];
                        currentButton.TurnOff();
                    }
                }
            }
            else if (_selectedButtonIndex < 10)
            {
                for (int i = 4; i < 10; i++)
                {
                    if (i != _selectedButtonIndex)
                    {
                        currentButton = (ToggleButton)_buttons[i];
                        currentButton.TurnOff();
                    }
                }

            }

        }

        private void HandleMenuMovement()
        {
            Direction direction = SceneInputManager.TranslateMenuMovementInput();
            MoveSelectedButtonIndex(direction);
            SceneManager.PrintCurrentScene();
        }

        private void MoveSelectedButtonIndex(Direction direction)
        {
            _buttons[_selectedButtonIndex].Deselect();

            if (_selectedButtonIndex < 4)
            {
                switch (direction)
                {
                    case Direction.Up:
                        if (_selectedButtonIndex == 0)
                        {
                            _selectedButtonIndex = 3;
                        }
                        else
                        {
                            _selectedButtonIndex--;
                        }
                        break;

                    case Direction.Down:
                        if (_selectedButtonIndex == 3)
                        {
                            _selectedButtonIndex = 0;
                        }
                        else
                        {
                            _selectedButtonIndex++;
                        }
                        break;

                    case Direction.Left:
                        _selectedButtonIndex += 10;
                        break;

                    case Direction.Right:
                        _selectedButtonIndex += 4;
                        break;
                }

            }
            else if (_selectedButtonIndex < 10)
            {
                switch (direction)
                {
                    case Direction.Up:
                        if (_selectedButtonIndex == 4)
                        {
                            _selectedButtonIndex = 9;
                        }
                        else
                        {
                            _selectedButtonIndex--;
                        }
                        break;

                    case Direction.Down:
                        if (_selectedButtonIndex == 9)
                        {
                            _selectedButtonIndex = 4;
                        }
                        else
                        {
                            _selectedButtonIndex++;
                        }
                        break;

                    case Direction.Left:
                        if (_selectedButtonIndex > 7)
                        {
                            _selectedButtonIndex = 3;
                        }
                        else
                        {
                            _selectedButtonIndex -= 4;
                        }
                        break;

                    case Direction.Right:
                        if (_selectedButtonIndex > 7)
                        {
                            _selectedButtonIndex = 13;
                        }
                        else
                        {
                            _selectedButtonIndex += 6;
                        }

                        break;
                }
            }
            else
            {
                switch (direction)
                {
                    case Direction.Up:
                        if (_selectedButtonIndex == 10)
                        {
                            _selectedButtonIndex = 13;
                        }
                        else
                        {
                            _selectedButtonIndex--;
                        }
                        break;

                    case Direction.Down:
                        if (_selectedButtonIndex == 13)
                        {
                            _selectedButtonIndex = 10;
                        }
                        else
                        {
                            _selectedButtonIndex++;
                        }
                        break;

                    case Direction.Left:
                        _selectedButtonIndex -= 6;
                        break;

                    case Direction.Right:
                        _selectedButtonIndex -= 10;
                        break;
                }
            }

            _buttons[_selectedButtonIndex].Select();
        }

        public override void PrintScene()
        {
            Console.ForegroundColor = _titleColor;
            Console.WriteLine(_title);
            
            Console.WriteLine();



            Console.WriteLine("Difficulty:");
            Printer.ColorReset();

            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine(_buttons[i]);
            }

            Printer.ColorReset();

            Console.SetCursorPosition(25, 2);
            int cursorRow = Console.CursorTop, cursorColumn = Console.CursorLeft;


            Console.ForegroundColor = _titleColor;
            Console.Write("Player Color:");
            Printer.ColorReset();

            for (int i = 4; i < 10; i++)
            {
                cursorRow++;
                Console.SetCursorPosition(cursorColumn, cursorRow);
                Console.Write(_buttons[i]);
            }

            Printer.ColorReset();

            Console.SetCursorPosition(50, 3);
            cursorRow = Console.CursorTop; cursorColumn = Console.CursorLeft;

            for (int i = 10; i < _buttons.Length; i++)
            {
                Console.Write(_buttons[i]);
                Printer.ColorReset();
                cursorRow++;
                Console.SetCursorPosition(cursorColumn, cursorRow);
            }

        }


    }
}
