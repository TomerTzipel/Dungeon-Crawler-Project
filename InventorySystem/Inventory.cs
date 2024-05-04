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
        public int Gold { get; private set; } = 500;
        public int Potions { get; private set; } = 1;


        public List<Item> EquipableItems { get; private set; } = new List<Item>(20);
        public List<Item> Trinkets { get; private set; } = new List<Item>(20);
        public Item[] EquippedItems { get; private set; } = new Item[6];
                                    //Additive Buffs   Multiplicative Buffs
        private int[][] _buffsValues = [new int[6], [100,100,100,100,100,100]];

        public Inventory(PlayerCombatEntity combatEntity) 
        {
            _combatEntity = combatEntity;
        }

        public void GainKey()
        {
            Keys++;
            _combatEntity.DoesHUDNeedReprint = true;
        }
        public void GainPotion()
        {
            Potions++;
            _combatEntity.DoesHUDNeedReprint = true;
        }

        public void GainGold(int amount)
        {
            Gold += amount;
            _combatEntity.DoesHUDNeedReprint = true;
        }

        public bool UseKey()
        {
            if(Keys == 0)
            {
                Printer.AddActionText(ActionTextType.General,"You have 0 keys...");
                return false;
            }

            Keys--;
            _combatEntity.DoesHUDNeedReprint = true;
            return true;
        }

        public bool UsePotion()
        {
            if (Potions == 0)
            {
                return false;
            }

            Potions--;
            _combatEntity.DoesHUDNeedReprint = true;
            return true;
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
            _combatEntity.DoesHUDNeedReprint = true;
        }

        public void AddItem(Item item)
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
            item.Equip();
            AddBuffs(item);

            EquipableItems.Remove(item);
        }

        public void Unequip(ItemType type)
        {
            int typeIndex = (int)type;
            Item item = EquippedItems[typeIndex];

            if (EquippedItems[typeIndex] == null)
            {
                return;
            }

            EquipableItems.Add(item);
            item.Unequip();
            RemoveBuffs(item);

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
                    GainHpBuff(buff.Value);
                    continue;
                }

                BuffType type = buff.Type;

                int buffTypeIndex = (int)type;
                int statTypeIndex = (int)buff.StatType;

                if(type == BuffType.Additive)
                {
                    _buffsValues[buffTypeIndex][statTypeIndex] += buff.Value;
                }
                else
                {
                    float buffValue = buff.Value/100f;

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
                    LoseHpBuff(buff.Value);
                    continue;
                }

                BuffType type = buff.Type;

                int buffTypeIndex = (int)type;
                int statTypeIndex = (int)buff.StatType;

                if (type == BuffType.Additive)
                {
                    _buffsValues[buffTypeIndex][statTypeIndex] -= buff.Value;
                }
                else
                {
                    float buffValue = buff.Value / 100f;

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
