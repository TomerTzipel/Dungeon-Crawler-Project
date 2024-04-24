

namespace UI_System
{
    public class ShopButton : Button
    {
        private int _cost;
        private LootType _type;
        private Item _item;

        private bool _wasBought = false;

        public ShopButton(int cost, LootType type, Item item) : base("SOLD OUT!")
        {
            _cost = cost;
            _type = type;
            _item = item;
        }

        public override bool OnClick()
        {
            Inventory inventory = PlayerManager.PlayerInventory;

            if (inventory.Gold >= _cost && !_wasBought)
            {
                _wasBought = true;
                inventory.SpendGold(_cost);
                Buy();
                return base.OnClick();
            }

            return false;

        }

        private void Buy()
        {
            Inventory inventory = PlayerManager.PlayerInventory;
            switch (_type)
            {
                case LootType.Key:
                    inventory.GainKey();
                    break;
                case LootType.Potion:
                    inventory.GainPotion();
                    break;
                case LootType.Item:
                    inventory.AddItem(_item);
                    break;
            }

        }

        public void Print()
        {
            Inventory inventory = PlayerManager.PlayerInventory;

            ConsoleColor printColor = ConsoleColor.DarkGreen;
            if (inventory.Gold < _cost) printColor = ConsoleColor.DarkRed;
            if (_wasBought) printColor = ConsoleColor.Black;
            Console.ForegroundColor = printColor;
            ToString();

            if (_wasBought)
            {
                Console.WriteLine(ToString()); 
                return;
            }

            int cursorRow = Console.CursorTop, cursorColumn = Console.CursorLeft;

            switch (_type)
            {
                case LootType.Key:
                    Console.Write("Key");
                    Console.SetCursorPosition(cursorColumn, cursorRow + 1);
                    break;

                case LootType.Potion:
                    Console.Write("Potion");
                    Console.SetCursorPosition(cursorColumn, cursorRow + 1);
                    break;

                case LootType.Item:
                    Console.Write(_item);
                    Console.SetCursorPosition(cursorColumn, cursorRow + 1);
                    _item.PrintBuffs();
                    break;
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"{_cost}G");
        }
    }
}
