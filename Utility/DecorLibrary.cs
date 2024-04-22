using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public enum DecorationType
    {
        Boulder, Tree, Grass, Pebble, Vase, Bush , HWall, VWall
    }

    public enum BiomeType
    {
        Mountain, Field, Forest, Ruin
    }
    public static class DecorLibrary
    {
        private static readonly int[] _biomeChances = [30,40,25,5];

        private static readonly DecorationType[][] _mountains = [
        [DecorationType.Boulder,
            DecorationType.Boulder,
            DecorationType.Boulder,
            DecorationType.Pebble,
            DecorationType.Pebble,
            DecorationType.Pebble,
            DecorationType.Pebble],

        [DecorationType.Boulder,
            DecorationType.Boulder,
            DecorationType.Boulder,
            DecorationType.Boulder,
            DecorationType.Pebble,
            DecorationType.Pebble,
            DecorationType.Pebble]
        ];

        private static readonly DecorationType[][] _fields = [
        [DecorationType.Bush,
            DecorationType.Bush,
            DecorationType.Tree,
            DecorationType.Grass,
            DecorationType.Grass,
            DecorationType.Grass,
            DecorationType.Grass],

        [DecorationType.Bush,
            DecorationType.Bush,
            DecorationType.Bush,
            DecorationType.Grass,
            DecorationType.Grass,
            DecorationType.Grass,
            DecorationType.Pebble]
        ];

        private static readonly DecorationType[][] _forests = [
        [DecorationType.Tree,
            DecorationType.Tree,
            DecorationType.Tree,
            DecorationType.Bush,
            DecorationType.Grass,
            DecorationType.Grass,
            DecorationType.Pebble],

        [DecorationType.Tree,
            DecorationType.Tree,
            DecorationType.Bush,
            DecorationType.Bush,
            DecorationType.Grass,
            DecorationType.Grass]
       ];



        private static readonly DecorationType[][] _ruins = [
        [DecorationType.Vase,
            DecorationType.Vase,
            DecorationType.VWall,
            DecorationType.VWall,
            DecorationType.HWall,
            DecorationType.HWall,
            DecorationType.Boulder],

        [DecorationType.Vase,
            DecorationType.Vase,
            DecorationType.Vase,
            DecorationType.VWall,
            DecorationType.VWall,
            DecorationType.HWall,
            DecorationType.HWall,
            DecorationType.Pebble,
            DecorationType.Pebble],

        ];
    

        public static BiomeType RandomBiome()
        {
            int chosenBiome = RandomWeightedIndex(_biomeChances);
  
            return (BiomeType)chosenBiome;
        }

        public static DecorationType[] GetDecorationByBiome(BiomeType type)
        {
            int chosenVarient;
            switch (type)
            {
                case BiomeType.Mountain:
                    chosenVarient = RandomIndex(_mountains.Length);
                    return _mountains[chosenVarient];
                    
                case BiomeType.Field:
                    chosenVarient = RandomIndex(_fields.Length);
                    return _fields[chosenVarient];

                case BiomeType.Forest:
                    chosenVarient = RandomIndex(_forests.Length);
                    return _forests[chosenVarient];

                case BiomeType.Ruin:
                    chosenVarient = RandomIndex(_ruins.Length);
                    return _ruins[chosenVarient];

            }

            return null;
        }

        public static EnemyType[] GetEnemiesByBiome(BiomeType type)
        {
            int chosenVarient;
            switch (type)
            {
                case BiomeType.Mountain:
                    chosenVarient = RandomIndex(EnemiesLibrary.MOUNTAIN_ENEMIES.Length);
                    return EnemiesLibrary.MOUNTAIN_ENEMIES[chosenVarient];

                case BiomeType.Field:
                    chosenVarient = RandomIndex(EnemiesLibrary.FIELD_ENEMIES.Length);
                    return EnemiesLibrary.FIELD_ENEMIES[chosenVarient];

                case BiomeType.Forest:
                    chosenVarient = RandomIndex(EnemiesLibrary.FOREST_ENEMIES.Length);
                    return EnemiesLibrary.FOREST_ENEMIES[chosenVarient];

                case BiomeType.Ruin:
                    chosenVarient = RandomIndex(EnemiesLibrary.RUIN_ENEMIES.Length);
                    return EnemiesLibrary.RUIN_ENEMIES[chosenVarient];

            }

            return null;
        }

    }
}
