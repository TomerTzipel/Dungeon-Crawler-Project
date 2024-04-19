

using InventorySystem;

namespace UI_System { 
    public class ItemButton : Button
    {
        private Item _item;

        public ItemButton(Item item) : base("No Item Equipped")
        {
            _item = item;
            if(item != null)
            {
                _text = item.ToString();
            }
            
        }

        public override bool OnClick()
        {
            if( _item == null)
            {
                return false;
            }

            if (_item.IsEquipped)
            {
                PlayerManager.PlayerElement.CombatEntity.Inventory.Unequip(_item.Type);
            }
            else
            {
                PlayerManager.PlayerElement.CombatEntity.Inventory.Equip(_item);
            }

            return base.OnClick();
        }

        public void Print()
        {
            int cursorRow = Console.CursorTop, cursorColumn = Console.CursorLeft;
            Console.WriteLine(ToString());

            if (_item == null) return;

            Console.SetCursorPosition(cursorColumn, cursorRow + 1);
            _item.PrintBuffs();
        }

    }
}
