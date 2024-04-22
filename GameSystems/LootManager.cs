

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GameSystems
{
    public enum LootOrigin
    {
        Bat, Ogre, Slime, Vase, Boulder, Bush
    }

    public enum LootType
    {
        Nothing, Gold, Key, Potion, Item
    }

    public static class LootManager
    {                                                  //Nothing    Gold    Key    Potion    Item          
        private static readonly int[] _batLootTable =     [50,       25,    19,      5,       1];
        private static readonly int[] _ogreLootTable =    [50,       15,    20,     10,       5];
        private static readonly int[] _slimeLootTable =   [50,       17,    10,     20,       3];
        private static readonly int[] _vaseLootTable =    [85,        0,     4,     10,       1];
        private static readonly int[] _bushLootTable =    [85,       10,     0,      4,       1];
        private static readonly int[] _boulderLootTable = [85,        4,     10,     0,       1];

        private static Dictionary<LootOrigin, int[]> _lootTable = new Dictionary<LootOrigin, int[]>(6);

        private static string _lootActionText = "";

        public static void SetUp()
        {
            _lootTable.Add(LootOrigin.Bat, _batLootTable);
            _lootTable.Add(LootOrigin.Ogre, _ogreLootTable);
            _lootTable.Add(LootOrigin.Slime, _slimeLootTable);
            _lootTable.Add(LootOrigin.Vase, _vaseLootTable);
            _lootTable.Add(LootOrigin.Boulder, _boulderLootTable);
            _lootTable.Add(LootOrigin.Bush, _bushLootTable);
        }

        public static void GenerateLoot(LootOrigin origin)
        {
            _lootTable.TryGetValue(origin, out int[] lootChances);
            LootType loot = (LootType)RandomWeightedIndex(lootChances);

            if (loot == LootType.Nothing) return;

            switch (origin)
            {
                case LootOrigin.Bat:
                    _lootActionText += "Slaying a bat you found - ";
                    break;
                case LootOrigin.Ogre:
                    _lootActionText += "Slaying an ogre you found - ";
                    break;
                case LootOrigin.Slime:
                    _lootActionText += "Slaying a slime you found - ";
                    break;
                case LootOrigin.Vase:
                    _lootActionText += "Destroying a vase you found - ";
                    break;
                case LootOrigin.Boulder:
                    _lootActionText += "Destroying a boulder you found - ";
                    break;
                case LootOrigin.Bush:
                    _lootActionText += "Destroying a bush you found - ";
                    break;
            }

            switch (loot)
            {
                case LootType.Gold:
                    RewardGold(origin);
                    break;
                case LootType.Key:
                    RewardKey();
                    break;
                case LootType.Potion:
                    RewardPotion();
                    break;
                case LootType.Item:
                    RewardItem();
                    break;
            }
        }

        private static void RewardGold(LootOrigin origin)
        {
            int amount = 10;

            switch (origin)
            {
                case LootOrigin.Bat:
                    amount = RandomRange(10,20);
                    break;

                case LootOrigin.Ogre:
                    amount = RandomRange(20, 60);
                    break;

                case LootOrigin.Slime:
                    amount = RandomRange(15, 40);
                    break;

                case LootOrigin.Vase:
                    amount = RandomRange(30, 80);
                    break;

                case LootOrigin.Boulder:
                    amount = RandomRange(10, 30);
                    break;

                case LootOrigin.Bush:
                    amount = RandomRange(5, 20);
                    break;
            }

            PlayerManager.PlayerElement.CombatEntity.Inventory.GainGold(amount);

            _lootActionText += $"{amount} Gold!";
            Printer.AddActionText(ActionTextType.Loot, _lootActionText);
            _lootActionText = "";

        }

        private static void RewardKey()
        {
            PlayerManager.PlayerElement.CombatEntity.Inventory.GainKey();

            _lootActionText += "a Key!";
            Printer.AddActionText(ActionTextType.Loot, _lootActionText);
            _lootActionText = "";
        }

        private static void RewardPotion()
        {
            PlayerManager.PlayerElement.CombatEntity.Inventory.GainPotion();

            _lootActionText += "a Potion!";
            Printer.AddActionText(ActionTextType.Loot, _lootActionText);
            _lootActionText = "";
        }

        private static void RewardItem()
        {
            //Change to randomly generated item
            Item item = new Item("A", ItemType.Trinket, [new Buff(BuffType.Additive, StatType.Hp, 10)]);
            PlayerManager.PlayerElement.CombatEntity.Inventory.AddItem(item);

            _lootActionText += item.ToString();
            Printer.AddActionText(ActionTextType.Loot, _lootActionText);
            _lootActionText = "";
        }

        public static void RewardItemByType(ItemType type)
        {

        }
    }
}
