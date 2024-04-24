


namespace SceneSystem
{
    public class ShopScene : SelectionMenuScene
    {
        private bool _isCurrentScene;

        protected ShopInputManager SceneInputManager
        {
            get
            {
                return (ShopInputManager)_inputManager;
            }
        }

        public ShopScene() : base(6,"",ConsoleColor.Yellow) 
        {
            _inputManager = new ShopInputManager();
        }

        private void GenerateShop()
        {
            _buttons[0] = new ShopButton(50,LootType.Key, null);
            _buttons[1] = new ShopButton(70, LootType.Potion, null);
            _buttons[2] = new ExitShopButton();
            _buttons[3] = new ShopButton(200, LootType.Item, ItemGenerator.GenerateRandomItem());
            _buttons[4] = new ShopButton(200, LootType.Item, ItemGenerator.GenerateRandomItem());
            _buttons[5] = new ShopButton(200, LootType.Item, ItemGenerator.GenerateRandomItem());
        }

        protected override void EnterScene()
        {
            if (!GameManager.IsShopActive)
            {
                GameManager.IsShopActive = true;
                GenerateShop();
            }
            
            _title =$"Shop   You have {PlayerManager.PlayerInventory.Gold}G";

            base.EnterScene();
            _isCurrentScene = true;
        }

        public override void SceneLoop()
        {
            EnterScene();

            while (!_wasButtonClicked && _isCurrentScene)
            {
                bool wasInputDetected = _inputManager.ReadInput();

                if (wasInputDetected)
                {
                    HandleInput();
                }
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

                case InputType.SceneChange:
                    HandleSceneChange();
                    break;

                case InputType.ButtonClick:
                    ActivateButton(_buttons[_selectedButtonIndex]);
                    break;

                default:
                    break;
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

            if (_selectedButtonIndex < 3)
            {
                switch (direction)
                {
                    case Direction.Up:
                        if (_selectedButtonIndex == 0)
                        {
                            _selectedButtonIndex = 2;
                        }
                        else
                        {
                            _selectedButtonIndex--;
                        }
                        break;

                    case Direction.Down:
                        if (_selectedButtonIndex == 2)
                        {
                            _selectedButtonIndex = 0;
                        }
                        else
                        {
                            _selectedButtonIndex++;
                        }
                        break;

                    case Direction.Left:
                        _selectedButtonIndex = 5;
                        break;

                    case Direction.Right:
                        _selectedButtonIndex = 3;
                        break;
                }
            }
            else
            {
                switch (direction)
                {
                    case Direction.Up:
                        _selectedButtonIndex = 2;
                        break;

                    case Direction.Down:
                        _selectedButtonIndex = 1;
                        break;

                    case Direction.Left:
                        if(_selectedButtonIndex == 3)
                        {
                            _selectedButtonIndex = 0;
                        }
                        else
                        {
                            _selectedButtonIndex--;
                        }
                        
                        break;

                    case Direction.Right:
                        if (_selectedButtonIndex == 5)
                        {
                            _selectedButtonIndex = 0;
                        }
                        else
                        {
                            _selectedButtonIndex++;
                        }
                        break;
                }
            }

            _buttons[_selectedButtonIndex].Select();
        }

        private void HandleSceneChange()
        {
            SceneType scene = SceneInputManager.TranslateSceneChangeInput();

            SceneManager.ChangeScene(scene);
            _isCurrentScene = false;
        }

        public override void PrintScene()
        {
            Console.ForegroundColor = _titleColor;
            Console.WriteLine(_title);
            Printer.ColorReset();
            Console.WriteLine();

            for (int i = 0; i < 2; i++)
            {
                ShopButton currentButton = (ShopButton)_buttons[i];
                currentButton.Print();
                Console.WriteLine();
            }

            Printer.ColorReset();
            Console.WriteLine(_buttons[2]);

            Console.SetCursorPosition(25, 2);
            int cursorRow = Console.CursorTop, cursorColumn = Console.CursorLeft;

            for (int i = 3; i < _buttons.Length; i++)
            {
                ShopButton currentButton = (ShopButton)_buttons[i];
                currentButton.Print();

                cursorColumn += 25;
                Console.SetCursorPosition(cursorColumn, cursorRow);
            }

        }
    }
}
