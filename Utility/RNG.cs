﻿
namespace Utility
{
    public static class RNG
    {
        public const int CONTINUE_CHANCE = 69;

        public const int BIOME_CHANCE = 50;

        private static readonly Random numberGenerator = new Random();

        public static int RandomPrecent()
        {
            return numberGenerator.Next(1,101);
        }

        
        public static int RandomIndex(int length)
        {
            return numberGenerator.Next(0, length);
        }

        public static int RandomRange(int min, int max)
        {
            return numberGenerator.Next(min, max + 1);
        }

        public static bool RollChance(int chance)
        {
            int roll = RandomPrecent();

            if (chance >= roll)
            {
                return true;
            }

            return false;
        }
        public static int RandomWeightedIndex(int[] chances)
        {
            int chanceAdder = 0;
            int chance;
            int result = RandomPrecent();

            for (int i = 0; i < chances.Length; i++)
            {
                chance = chances[i];

                if (chance + chanceAdder >= result)
                {
                    return i;
                }

                chanceAdder += chance;

            }

            return -1; //Will never reach unless chances array doesn't sum to 100
        }


    }
}
