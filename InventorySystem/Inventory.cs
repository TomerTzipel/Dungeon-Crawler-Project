using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem
{
    public class Inventory
    {
        private PlayerCombatEntity _combatEntity;

        public int Keys { get; private set; } = 1;
        public int Gold { get; private set; } = 10;

        public List<Item> EquipableItems { get; private set; } = new List<Item>(8);
        public List<Item> Trinkets { get; private set; } = new List<Item>(8);
        public Item[] EquippedItems { get; private set; } = new Item[6];

        private int[][] _buffsValues = [new int[6], [100,100,100,100,100,100]];

        public Inventory(PlayerCombatEntity combatEntity) 
        {
            _combatEntity = combatEntity;
        }

        public bool HasKey
        {
            get { return Keys > 0; }
        }

        public void GainKeys(int amount)
        {
            Keys += amount;
        }
        public void UseKey()
        {
            Keys--;
        }

        public void GainGold(int amount)
        {
            Gold += amount;
        }

        public bool SpendGold(int amount)
        {
            int newAmount = Gold - amount;

            if(newAmount < 0)
            {
                return false;
            }

            LoseGold(amount);

            return true;
        }

        public void LoseGold(int amount)
        {
            Gold -= amount;

            if(Gold < 0)
            {
                Gold = 0;
            }
        }

        public void GetItem(Item item)
        {
            ItemType type = item.Type;

            if (type == ItemType.Trinket)
            {
                Trinkets.Add(item);
                AddBuffs(item);
                return;
            }

            int typeIndex = (int)type;

            if (EquippedItems[typeIndex] == null)
            {
                Equip(item);
                return;
            }

            EquipableItems.Add(item);
        }

        public void Equip(Item item)
        {

            ItemType type = item.Type;
            int typeIndex = (int)type;

            if (EquippedItems[typeIndex] != null)
            {
                Unequip(type);
            }

            EquippedItems[typeIndex] = item;
            AddBuffs(item);

            EquipableItems.Remove(item);
        }

        public void Unequip(ItemType type)
        {
            int typeIndex = (int)type;

            if(EquippedItems[typeIndex] == null)
            {
                return;
            }

            EquipableItems.Add(EquippedItems[typeIndex]);

            RemoveBuffs(EquippedItems[typeIndex]);
            EquippedItems[typeIndex] = null;
            
        }

        public int GetBuff(BuffType type, StatType statType) 
        {
            return _buffsValues[(int)type][(int)statType];
        }

        private void AddBuffs(Item item)
        {
            List<Buff> itemBuffs = item.Buffs;

            foreach (Buff buff in itemBuffs)
            {

                if(buff.StatType == StatType.Hp)
                {
                    GainHpBuff(buff.value);
                    continue;
                }

                BuffType type = buff.Type;

                int buffTypeIndex = (int)type;
                int statTypeIndex = (int)buff.StatType;

                if(type == BuffType.Additive)
                {
                    _buffsValues[buffTypeIndex][statTypeIndex] += buff.value;
                }
                else
                {
                    float buffValue = buff.value/100f;

                    float currentValue = _buffsValues[buffTypeIndex][statTypeIndex];

                    float newValue = currentValue * buffValue;

                    _buffsValues[buffTypeIndex][statTypeIndex] = (int)newValue;
                }
            }
        }

        private void RemoveBuffs(Item item)
        {
            List<Buff> itemBuffs = item.Buffs;

            foreach (Buff buff in itemBuffs)
            {
                if (buff.StatType == StatType.Hp)
                {
                    LoseHpBuff(buff.value);
                    continue;
                }

                BuffType type = buff.Type;

                int buffTypeIndex = (int)type;
                int statTypeIndex = (int)buff.StatType;

                if (type == BuffType.Additive)
                {
                    _buffsValues[buffTypeIndex][statTypeIndex] -= buff.value;
                }
                else
                {
                    float buffValue = buff.value / 100f;

                    float currentValue = _buffsValues[buffTypeIndex][statTypeIndex];

                    float newValue = currentValue / buffValue;

                    _buffsValues[buffTypeIndex][statTypeIndex] = (int)newValue;
                }
            }
        }
        private void GainHpBuff(int value)
        {
            _combatEntity.GainHpBuff(value);
        }

        private void LoseHpBuff(int value)
        {
            _combatEntity.LoseHpBuff(value);
        }

    }
}
