using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{

    public enum EnemyType
    {
        Bat, Slime, Ogre
    }

    public static class EnemiesLibrary
    {

        private static readonly EnemyType[][] _easyEncounter = [  [EnemyType.Bat,EnemyType.Bat,EnemyType.Bat],
                                                                [EnemyType.Ogre],
                                                                [EnemyType.Slime]];

        private static readonly EnemyType[][] _mediumEncounter = [[EnemyType.Bat, EnemyType.Bat, EnemyType.Bat]];

        private static readonly EnemyType[][] _hardEncounter = [[EnemyType.Bat, EnemyType.Bat, EnemyType.Bat]];

        public static readonly EnemyType[][] MOUNTAIN_ENEMIES = [[EnemyType.Ogre],[]];

        public static readonly EnemyType[][] FIELD_ENEMIES = [[EnemyType.Slime],[]];

        public static readonly EnemyType[][] RUIN_ENEMIES = [[EnemyType.Bat],[]];

        public static readonly EnemyType[][] FOREST_ENEMIES = [[EnemyType.Bat],[]];

        //BAT STATS
        public  const int BAT_HP = 20;
        public  const int BAT_ARMOR = 0;

        public const int BAT_DAMAGE = 10;
        public const int BAT_ACCURACY = 50;
        public const int BAT_EVASION = 40;
        public const int BAT_MULTIHIT = 10;
        public const int BAT_PIERCE = 0;

        public const float BAT_MOVEMENT_SPEED = 1f;
        public const int BAT_RANGE = 3;

        //SLIME STATS
        public const int SLIME_ACCURACY = 100;
        public const int SLIME_ARMOR = 0;
        public const int SLIME_RANGE = 4;
        public const int SLIME_PIERCE = 0;

        public const int SLIME1_HP = 10;
        public const int SLIME1_DAMAGE = 10;
        public const int SLIME1_EVASION = 50;
        public const int SLIME1_MULTIHIT = 20;
        
        public const int SLIME2_DAMAGE = 20;
        public const int SLIME2_HP = 20;
        public const int SLIME2_EVASION = 30;
        public const int SLIME2_MULTIHIT = 10;
        
        public const int SLIME3_DAMAGE = 30;
        public const int SLIME3_HP = 30;
        public const int SLIME3_EVASION = 10;
        public const int SLIME3_MULTIHIT = 0;



        //OGRE STATS
        public const int OGRE_HP = 100;
        public const int OGRE_ARMOR = 10;

        public const int OGRE_DAMAGE = 50;
        public const int OGRE_ACCURACY = 80;
        public const int OGRE_EVASION = 10;
        public const int OGRE_MULTIHIT = 20;
        public const int OGRE_PIERCE = 20;

        public const float OGRE_MOVEMENT_SPEED = 3.5f;
        public const int OGRE_RANGE = 5;

        public static float DifficultyScaling { 
            get
            {
                switch (LevelManager.CurrentDifficulty)
                {
                    case Difficulty.Medium:
                        return 1.25f;
                    case Difficulty.Hard:
                        return 1.5f;
                }

                return 1f;
            }
        }

        public static EnemyType[] GetEnemiesByCurrentLevel()
        {
            Difficulty difficulty = LevelManager.CurrentDifficulty;
            return PullEnemiesByDifficulty(difficulty);
        }

        private static EnemyType[] PullEnemiesByDifficulty(Difficulty difficulty)
        {
            int chosenEnemies;
            switch (difficulty)
            {
                case Difficulty.Easy:
                    chosenEnemies = RandomIndex(_easyEncounter.Length);
                    return _easyEncounter[chosenEnemies];

                case Difficulty.Medium:
                    chosenEnemies = RandomIndex(_mediumEncounter.Length);
                    return _mediumEncounter[chosenEnemies];

                case Difficulty.Hard:
                    chosenEnemies = RandomIndex(_hardEncounter.Length);
                    return _hardEncounter[chosenEnemies];
            }

            return null;
            
        }
        public static EnemyType[] GetEnemiesByBiome(BiomeType type)
        {
            int chosenVarient;
            switch (type)
            {
                case BiomeType.Mountain:
                    chosenVarient = RandomIndex(MOUNTAIN_ENEMIES.Length);
                    return MOUNTAIN_ENEMIES[chosenVarient];

                case BiomeType.Field:
                    chosenVarient = RandomIndex(FIELD_ENEMIES.Length);
                    return FIELD_ENEMIES[chosenVarient];

                case BiomeType.Forest:
                    chosenVarient = RandomIndex(FOREST_ENEMIES.Length);
                    return FOREST_ENEMIES[chosenVarient];

                case BiomeType.Ruin:
                    chosenVarient = RandomIndex(RUIN_ENEMIES.Length);
                    return RUIN_ENEMIES[chosenVarient];

            }

            return null;
        }


    }
}
