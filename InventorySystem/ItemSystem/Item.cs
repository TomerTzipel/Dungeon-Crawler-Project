

namespace InventorySystem
{
   

    public enum BuffType
    {
        Additive, Multiplicative
    }

    public enum StatType
    {
        Damage, Armor, Evasion, Accuracy, Pierce, Multihit, Hp
    }

    public struct Buff
    {
        public BuffType Type { init; get; }
        public StatType StatType { init; get; }
        public int Value { init; get; } // Multiplicative buffs are diveded by 100 before use as they reprecent precentage

        public Buff(BuffType type, StatType statType, int value)
        {
            Type = type;
            StatType = statType;
            Value = value;
        }

        public override string ToString()
        {
            if(Type == BuffType.Additive)
            {
                return $"A,{Value},{StatType}";
            }

            return $"M,{Value/100f},{StatType}";
        }
    }

    public enum ItemType
    {
        Head, Body, Legs, Arms, Ring, Necklace, Trinket
    }
    public class Item
    {
        public string Name { get; private set; }    
        public ItemType Type { get; private set;}
        public List<Buff> Buffs { get; private set; }
        
        public bool IsEquipped {  get; private set; } = false;

        public Item(string name, ItemType type, List<Buff> buffs )
        {
            Name = name;
            Type = type;
            Buffs = buffs;
        }

        public void PrintBuffs()
        {
            int cursorRow = Console.CursorTop, cursorColumn = Console.CursorLeft;

            for (int i = 0; i < Buffs.Count; i++)
            {
                Buff buff = Buffs[i];
                Console.WriteLine(buff);
                Console.SetCursorPosition(cursorColumn, cursorRow + i + 1);
            }
        }

        public void Equip()
        {
            IsEquipped = true;
        }

        public void Unequip()
        {
            IsEquipped = false;
        }

        public override string ToString()
        {
            return $"{Name},{Type}";
        }
    }
}
