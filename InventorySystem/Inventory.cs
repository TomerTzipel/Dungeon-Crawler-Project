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
        public int Potions { get; private set; } = 0;


        public List<Item> EquipableItems { get; private set; } = new List<Item>(20);
        public List<Item> Trinkets { get; private set; } = new List<Item>(20);
        public Item[] EquippedItems { get; private set; } = new Item[6];

        private int[][] _buffsValues = [new int[6], [100,100,100,100,100,100]];

        public Inventory(PlayerCombatEntity combatEntity) 
        {
            _combatEntity = combatEntity;
            GetItem(new Item("Cool Helmet",ItemType.Head,[new Buff(BuffType.Additive,StatType.Hp,10)]));
            GetItem(new Item("Cool Chestplate", ItemType.Body, [new Buff(BuffType.Additive, StatType.Hp, 10)]));
            GetItem(new Item("Cool Gauntlets", ItemType.Arms,[ new Buff(BuffType.Additive, StatType.Hp, 10) ]));
            GetItem(new Item("Cool Ring", ItemType.Ring, [ new Buff(BuffType.Additive, StatType.Hp, 10) ]));
            GetItem(new Item("Cool Leggings", ItemType.Legs, [new Buff(BuffType.Additive, StatType.Hp, 10) ]));
            GetItem(new Item("Cool Necklace", ItemType.Necklace, [ new Buff(BuffType.Additive, StatType.Hp, 10) ]));


            GetItem(new Item("A", ItemType.Trinket, [new Buff(BuffType.Additive, StatType.Hp, 10)]));
            GetItem(new Item("B", ItemType.Trinket, [new Buff(BuffType.Additive, StatType.Hp, 10)]));
            GetItem(new Item("C", ItemType.Trinket, [new Buff(BuffType.Additive, StatType.Hp, 10)]));
            GetItem(new Item("D", ItemType.Trinket, [new Buff(BuffType.Additive, StatType.Hp, 10)]));
            GetItem(new Item("E", ItemType.Trinket, [new Buff(BuffType.Additive, StatType.Hp, 10)]));
            GetItem(new Item("F", ItemType.Trinket, [new Buff(BuffType.Additive, StatType.Hp, 10)]));
            GetItem(new Item("G", ItemType.Trinket, [new Buff(BuffType.Additive, StatType.Hp, 10)]));
            GetItem(new Item("H", ItemType.Trinket, [new Buff(BuffType.Additive, StatType.Hp, 10)]));
            GetItem(new Item("I", ItemType.Trinket, [new Buff(BuffType.Additive, StatType.Hp, 10)]));
            GetItem(new Item("K", ItemType.Trinket, [new Buff(BuffType.Additive, StatType.Hp, 10)]));
            GetItem(new Item("J", ItemType.Trinket, [new Buff(BuffType.Additive, StatType.Hp, 10)]));
            GetItem(new Item("L", ItemType.Trinket, [new Buff(BuffType.Additive, StatType.Hp, 10)]));
            GetItem(new Item("M", ItemType.Trinket, [new Buff(BuffType.Additive, StatType.Hp, 10)]));
            GetItem(new Item("O", ItemType.Trinket, [new Buff(BuffType.Additive, StatType.Hp, 10)]));
            GetItem(new Item("P", ItemType.Trinket, [new Buff(BuffType.Additive, StatType.Hp, 10)]));
            GetItem(new Item("Q", ItemType.Trinket, [new Buff(BuffType.Additive, StatType.Hp, 10)]));

      
        }

        public void GainKeys(int amount)
        {
            Keys += amount;
            _combatEntity.DoesHUDNeedReprint = true;
        }
        public void GainPotions(int amount)
        {
            Potions += amount;
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
