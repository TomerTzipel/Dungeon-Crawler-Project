
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

        
        public static int RandomIndex(int range)
        {
            return numberGenerator.Next(0, range);
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



    }
}
