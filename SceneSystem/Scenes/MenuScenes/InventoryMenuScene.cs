
namespace SceneSystem
{
    public class InventoryMenuScene : Scene
    {
        private readonly ConsoleColor _titlesColor = ConsoleColor.Yellow;

        private ItemButton[] _equippedItemsShowcase = new ItemButton[6];

        private List<ItemButton[]> _equipableItemsShowcase;

        private bool _isCurrentScene;

        private Point selectedButtonMarkerPosition = new Point(0, -1);
        private bool _wasButtonClicked;

        protected InventoryMenuInputManger SceneInputManager
        {
            get
            {
                return (InventoryMenuInputManger)_inputManager;
            }
        }

        public InventoryMenuScene() : base(new InventoryMenuInputManger()) { }

        protected override void EnterScene()
        { 
            GenerateEquippedItemsShowcase();
            GenerateEquipableItemsShowcase();

            _isCurrentScene = true;
            _wasButtonClicked = false;
            RepositionSelectedMarker();

            SceneManager.PrintCurrentScene();
        }

        public override void SceneLoop()
        {
            EnterScene();
            while (_isCurrentScene && !_wasButtonClicked) 
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

                case InputType.SceneChange:
                    HandleSceneChange();
                    break;

                case InputType.ButtonClick:
                    HandleButtonActivation();
                    break;

                default:
                    break;
            }
        }
        private void HandleMenuMovement()
        {
            Direction direction = SceneInputManager.TranslateMenuMovementInput();
            MoveSelectedButtonMarker(direction);
            SceneManager.PrintCurrentScene();
        }
        private void HandleSceneChange()
        {
            SceneType scene = SceneInputManager.TranslateSceneChangeInput();

            SceneManager.ChangeScene(scene);
            _isCurrentScene = false;
        }

        private void HandleButtonActivation()
        {
            ItemButton selectedButton = GetSelectedButton();
            bool clickResult = selectedButton.OnClick();

            if (clickResult)
            {
                _wasButtonClicked = true;
            }

        }
        private void MoveSelectedButtonMarker(Direction direction)
        {
            GetSelectedButton().Deselect();

            do
            {
                selectedButtonMarkerPosition.MovePointInDirection(direction);

                if (selectedButtonMarkerPosition.Y == -2)
                {
                    selectedButtonMarkerPosition.Y = _equipableItemsShowcase.Count - 1;
                }
                else if (selectedButtonMarkerPosition.Y == _equipableItemsShowcase.Count)
                {
                    selectedButtonMarkerPosition.Y = -1;
                }

                if (selectedButtonMarkerPosition.X == -1)
                {
                    selectedButtonMarkerPosition.X = 5;
                }
                else if (selectedButtonMarkerPosition.X == 6)
                {
                    selectedButtonMarkerPosition.X = 0;
                }

            } while (GetSelectedButton() == null);

            GetSelectedButton().Select();
        }

        private ItemButton GetSelectedButton()
        {
            if(selectedButtonMarkerPosition.Y == -1)
            {
                return _equippedItemsShowcase[selectedButtonMarkerPosition.X];
            }

            if(_equipableItemsShowcase.Count == 0)
            {
                return null;
            }

            return _equipableItemsShowcase[selectedButtonMarkerPosition.Y][selectedButtonMarkerPosition.X];
        }

        public override void PrintScene()
        {
            Console.ForegroundColor = _titlesColor;
            Console.WriteLine("Equipped Items:");
            Printer.ColorReset();
            Console.WriteLine();
            PrintEquippedItemsShowcase();
            Console.WriteLine(); Console.WriteLine();
            Console.WriteLine(); Console.WriteLine(); Console.WriteLine();

            Console.ForegroundColor = _titlesColor;
            Console.WriteLine("Equipable Items:");
            Printer.ColorReset();
            Console.WriteLine();
            PrintEquipableItemsShowcase();

            Console.ForegroundColor = _titlesColor;
            Console.WriteLine("Trinkets:");
            Printer.ColorReset();
            Console.WriteLine();
            PrintTrinkets();
        }

        private void PrintEquippedItemsShowcase()
        {
            int cursorRow = Console.CursorTop;
            int cursorColumn = Console.CursorLeft;

            for (int i = 0; i < _equippedItemsShowcase.Length; i++)
            {
                ItemButton currentButton = _equippedItemsShowcase[i];

                currentButton.Print();
                Printer.ColorReset();

                cursorColumn += 25;
                Console.SetCursorPosition(cursorColumn, cursorRow);
               //Printer.SetPrinterPosition(cursorColumn, cursorRow);
            }
        }
        private void PrintEquipableItemsShowcase()
        {
            int cursorRow = Console.CursorTop;
            int cursorColumn = Console.CursorLeft;

            foreach(ItemButton[] buttons in  _equipableItemsShowcase)
            {
                for (int i = 0;i < buttons.Length;i++)
                {
                    ItemButton currentButton = buttons[i];

                    if(currentButton == null)
                    {
                        continue;
                    }

                    currentButton.Print();
                    Printer.ColorReset();

                    cursorColumn += 25;
                    Console.SetCursorPosition(cursorColumn, cursorRow);
                    //Printer.SetPrinterPosition(cursorColumn, cursorRow);
                }
                cursorColumn = 0;
                Console.WriteLine(); Console.WriteLine();
                Console.WriteLine(); Console.WriteLine(); Console.WriteLine();
                cursorRow = Console.CursorTop;
            }
        }
        private void PrintTrinkets()
        {
            List<Item> trinkets = PlayerManager.PlayerInventory.Trinkets;

            int cursorRow, cursorColumn;

            int count = 0;

            foreach (Item trinket in trinkets)
            {
                cursorRow = Console.CursorTop; cursorColumn = Console.CursorLeft;
                Console.WriteLine(trinket);

                Console.SetCursorPosition(cursorColumn, cursorRow + 1);
                //Printer.SetPrinterPosition(cursorColumn, cursorRow + 1);
                trinket.PrintBuffs();

                cursorColumn += 25;
                Console.SetCursorPosition(cursorColumn, cursorRow);
                //Printer.SetPrinterPosition(cursorColumn, cursorRow);

                count++;

                if(count != 0 && count % 6 == 0)
                {
                    cursorColumn = 0;
                    Console.WriteLine(); Console.WriteLine();
                    Console.WriteLine(); Console.WriteLine(); Console.WriteLine();
                    cursorRow = Console.CursorTop;
                }
               
            }
        }
        
        private void GenerateEquippedItemsShowcase()
        {
            Item[] equippedItems = PlayerManager.PlayerInventory.EquippedItems;

            for (int i = 0; i < equippedItems.Length; i++)
            {
                _equippedItemsShowcase[i] = new ItemButton(equippedItems[i]);
            }
        }
        private void GenerateEquipableItemsShowcase()
        {
            List<Item> equipableItems = PlayerManager.PlayerInventory.EquipableItems;
            int count = 0;

            _equipableItemsShowcase = new List<ItemButton[]>(5);

            foreach (Item item in equipableItems)
            {
                if(count % 6 == 0)
                {
                    _equipableItemsShowcase.Add(new ItemButton[6]);
                }

                _equipableItemsShowcase[count / 6][count % 6] = new ItemButton(item);

                count++;
            }
        }

        private void RepositionSelectedMarker()
        {
            if (GetSelectedButton() == null)
            {
                selectedButtonMarkerPosition.MovePointInDirection(Direction.Left);
                if (selectedButtonMarkerPosition.X == -1)
                {
                    selectedButtonMarkerPosition.X = 5;
                    selectedButtonMarkerPosition.MovePointInDirection(Direction.Up);
                }
                
            }

            GetSelectedButton().Select();
        }

        
    }
}
