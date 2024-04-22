using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystems
{
    public static class ItemGenerator
    {
        public static Item GenerateRandomItem()
        {
                                                         //Head, Body, Legs, Arms, Ring, Necklace, Trinket 
            ItemType type = (ItemType)RandomWeightedIndex([15,   15,   15,   15,   15,     15,      10]);
            return GenerateItem(type);
        }
        public static Item GenerateTrinket()
        {
            return GenerateItem(ItemType.Trinket);
        }
        public static Item GenerateEquipment()
        {
            ItemType type = (ItemType)RandomIndex(6);
            return GenerateItem(type);
        }

        private static Item GenerateItem(ItemType type)
        {
            List<Buff> buffs = GenerateBuffsByLevel();
            string name = GenerateName(type);
            return new Item(name,type,buffs);
        }

        private static string GenerateName(ItemType type)
        {
            string name = "ITEM";

            switch (type)
            {
                case ItemType.Head:

                    break;

                case ItemType.Body:

                    break;

                case ItemType.Legs:

                    break;

                case ItemType.Arms:

                    break;

                case ItemType.Ring:

                    break;

                case ItemType.Necklace:

                    break;

                case ItemType.Trinket:

                    break;


            }

            return name;
        }

        private static List<Buff> GenerateBuffsByLevel()
        {
            Difficulty difficulty = LevelManager.CurrentDifficulty;
            int amountOfBuffs = 1 + (int)difficulty;

            List<Buff> buffs = new List<Buff>(amountOfBuffs);

            for (int i = 0; i < amountOfBuffs; i++)
            {
                Buff buff = GenerateBuff(difficulty);
                buffs.Add(buff);
            }

            return buffs;

        }

        private static Buff GenerateBuff(Difficulty difficulty)
        {
                                                        //Dmg, Armor, Evasion, Accuracy, Pierce, MultiHit, HP 
            StatType stat = (StatType)RandomWeightedIndex([20,  10,      10,      15,      15,      10,   20]);

            BuffType type = BuffType.Additive;
            if (stat != StatType.Hp)
            {
                                                 //Additive,  Multplicative
                type = (BuffType)RandomWeightedIndex([75,         25]);
            }                                         

            int value = GenerateBuffValue(stat, type, difficulty);

            return new Buff(type,stat,value);
        }

        private static int GenerateBuffValue(StatType stat, BuffType type, Difficulty difficulty)
        {
            int value = 0;
            switch (difficulty)
            {
                case Difficulty.Easy:
                    value = GenerateEasyBuffValue(stat, type);
                    break;

                case Difficulty.Medium:
                    value = GenerateMediumBuffValue(stat, type);
                    break;

                case Difficulty.Hard:
                    value = GenerateHardBuffValue(stat, type);
                    break;
            }

            return value;

        }

        private static int GenerateHardBuffValue(StatType stat, BuffType type)
        {
            int value = 0;

            switch (type)
            {
                case BuffType.Additive:

                    switch (stat)
                    {
                        case StatType.Damage:
                            value = RandomRange(70, 100);
                            break;

                        case StatType.Armor:
                            value = RandomRange(20, 30);
                            break;

                        case StatType.Evasion:
                            value = RandomRange(20, 25);
                            break;

                        case StatType.Accuracy:
                            value = RandomRange(30, 40);
                            break;

                        case StatType.Pierce:
                            value = RandomRange(50, 80);
                            break;

                        case StatType.Multihit:
                            value = RandomRange(30, 50);
                            break;

                        case StatType.Hp:
                            value = RandomRange(300, 500);
                            break;

                    }
                    break;

                case BuffType.Multiplicative:

                    switch (stat)
                    {
                        case StatType.Damage:
                            value = RandomRange(150, 200);
                            break;
                        case StatType.Armor:
                            value = RandomRange(120, 130);
                            break;
                        case StatType.Evasion:
                            value = 130;
                            break;
                        case StatType.Accuracy:
                            value = RandomRange(125, 150);
                            break;
                        case StatType.Pierce:
                            value = RandomRange(150, 200);
                            break;
                        case StatType.Multihit:
                            value = 130;
                            break;
                    }
                    break;
            }

            return value;
        }

        private static int GenerateMediumBuffValue(StatType stat, BuffType type)
        {
            int value = 0;

            switch (type)
            {
                case BuffType.Additive:

                    switch (stat)
                    {
                        case StatType.Damage:
                            value = RandomRange(30, 60);
                            break;

                        case StatType.Armor:
                            value = RandomRange(10, 20);
                            break;

                        case StatType.Evasion:
                            value = RandomRange(10, 15);
                            break;

                        case StatType.Accuracy:
                            value = RandomRange(20, 30);
                            break;

                        case StatType.Pierce:
                            value = RandomRange(20, 40);
                            break;

                        case StatType.Multihit:
                            value = RandomRange(20, 30);
                            break;

                        case StatType.Hp:
                            value = RandomRange(100, 300);
                            break;

                    }
                    break;

                case BuffType.Multiplicative:

                    switch (stat)
                    {
                        case StatType.Damage:
                            value = RandomRange(125, 150);
                            break;
                        case StatType.Armor:
                            value = RandomRange(110, 120);
                            break;
                        case StatType.Evasion:
                            value = 120;
                            break;
                        case StatType.Accuracy:
                            value = RandomRange(115, 125);
                            break;
                        case StatType.Pierce:
                            value = RandomRange(125, 150);
                            break;
                        case StatType.Multihit:
                            value = 120;
                            break;
                    }
                    break;
            }

            return value;
        }

        private static int GenerateEasyBuffValue(StatType stat, BuffType type)
        {
            int value = 0;

            switch (type)
            {
                case BuffType.Additive:

                    switch (stat)
                    {
                        case StatType.Damage:
                            value = RandomRange(1, 30);
                            break;

                        case StatType.Armor:
                            value = RandomRange(1, 10);
                            break;

                        case StatType.Evasion:
                            value = RandomRange(1, 10);
                            break;

                        case StatType.Accuracy:
                            value = RandomRange(1, 20);
                            break;

                        case StatType.Pierce:
                            value = RandomRange(1, 20);
                            break;

                        case StatType.Multihit:
                            value = RandomRange(1, 20);
                            break;

                        case StatType.Hp:
                            value = RandomRange(1, 100);
                            break;

                    }
                    break;

                case BuffType.Multiplicative:

                    switch (stat)
                    {
                        case StatType.Damage:
                            value = RandomRange(105, 115);
                            break;
                        case StatType.Armor:
                            value = RandomRange(101, 110);
                            break;
                        case StatType.Evasion:
                            value = 110;
                            break;
                        case StatType.Accuracy:
                            value = RandomRange(101, 115);
                            break;
                        case StatType.Pierce:
                            value = RandomRange(101, 125);
                            break;
                        case StatType.Multihit:
                            value = 110;
                            break;
                    }
                    break;
            }

            return value;
        }
    }
}
