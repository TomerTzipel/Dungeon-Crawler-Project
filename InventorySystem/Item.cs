using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public BuffType Type;
        public StatType StatType;
        public int value; // Multiplicative buffs are diveded by 100 before use as they reprecent precentage

        public override string ToString()
        {
            if(Type == BuffType.Additive)
            {
                return $"Stat:{Type},Value:{value},Buff:{StatType}";
            }

            return $"Stat:{Type},Value:{value/100f},Buff:{StatType}";
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
        public List<Buff> Buffs { get; private set; } = new List<Buff>(4);


        public string GetBuffsText()
        {
            string str = "";

            foreach(Buff buff in Buffs)
            {
                str += buff.ToString() + "\n";
            }

            return str;

        }

        public override string ToString()
        {
            return $"Name:{Name},Type:{Type}";
        }
    }
}
