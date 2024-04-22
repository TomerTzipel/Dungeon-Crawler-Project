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

        private static readonly EnemyType[][] _easyEnemies = [  [EnemyType.Bat,EnemyType.Bat,EnemyType.Bat],
                                                                [EnemyType.Ogre],
                                                                [EnemyType.Slime]];

        private static readonly EnemyType[][] _mediumEnemies = [[EnemyType.Bat, EnemyType.Bat, EnemyType.Bat]];

        private static readonly EnemyType[][] _hardEnemies = [[EnemyType.Bat, EnemyType.Bat, EnemyType.Bat]];

        public static readonly EnemyType[][] MOUNTAIN_ENEMIES = [[EnemyType.Ogre],[]];

        public static readonly EnemyType[][] FIELD_ENEMIES = [[EnemyType.Slime],[]];

        public static readonly EnemyType[][] RUIN_ENEMIES = [[EnemyType.Bat],[]];

        public static readonly EnemyType[][] FOREST_ENEMIES = [[EnemyType.Bat],[]];

        //BAT STATS
        public  const int BAT_HP = 20;
        public  const int BAT_ARMOR = 0;

        public const int BAT_DAMAGE = 8;
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

        public const int OGRE_DAMAGE = 10;
        public const int OGRE_ACCURACY = 80;
        public const int OGRE_EVASION = 10;
        public const int OGRE_MULTIHIT = 20;
        public const int OGRE_PIERCE = 20;

        public const float OGRE_MOVEMENT_SPEED = 3.5f;
        public const int OGRE_RANGE = 5;

        public static EnemyType[] GetEnemiesByCurrentLevel()
        {
            switch (LevelManager.CurrentLevelNumber)
            {
                case 1: 
                case 2:   
                case 3:
                    return PullEnemiesByDifficulty(1);
                case 4:
                case 5:
                case 6:
                case 7:
                    return PullEnemiesByDifficulty(2);
                case 8:
                case 9:
                case 10:
                    return PullEnemiesByDifficulty(3);
            }

            return null;
        }

        private static EnemyType[] PullEnemiesByDifficulty(int difficulty)
        {
            int chosenEnemies;
            switch (difficulty)
            {
                case 1:
                    chosenEnemies = RandomIndex(_easyEnemies.Length);
                    return _easyEnemies[chosenEnemies];

                case 2:
                    chosenEnemies = RandomIndex(_mediumEnemies.Length);
                    return _mediumEnemies[chosenEnemies];

                case 3:
                    chosenEnemies = RandomIndex(_hardEnemies.Length);
                    return _hardEnemies[chosenEnemies];
            }

            return null;
            
        }



    }
}
