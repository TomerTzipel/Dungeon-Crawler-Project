﻿

namespace GameSystem
{

    public enum Difficulty
    {
        Easy, Normal, Hard, Dynamic
    }

    public static class LevelManager
    {
        public static Level CurrentLevel { get; private set; }

        public static bool IsLevelActive { get; set; } = false;

        

        private static readonly int[] _levelNumberOfSections = [38, 48, 72, 84, 97, 120, 132, 172, 194, 240];
        private static readonly int[] _levelSizes = [8, 9, 11, 12, 13, 14, 15, 17, 18, 20];
                                                            //Puzzles,   Enemy,  Chests, Traps
        private static readonly int[][] _levelCompositions = [[  1   ,     3    ,   1  ,   3  ],
                                                              [  1   ,     3    ,   1  ,   3  ],
                                                              [  1   ,     4    ,   1  ,   4  ],
                                                              [  1   ,     4    ,   2  ,   4  ],
                                                              [  1   ,     5    ,   2  ,   5  ],
                                                              [  2   ,     5    ,   2  ,   5  ],
                                                              [  2   ,     6    ,   2  ,   6  ],
                                                              [  2   ,     6    ,   3  ,   6  ],
                                                              [  2   ,     7    ,   3  ,   7  ],
                                                              [  2   ,     8    ,   3  ,   7  ]];
        public static int CurrentLevelValue { get; private set; }

        public static int CurrentLevelNumber
        {
            get { return CurrentLevelValue + 1; }
        }

        public static Difficulty CurrentDifficulty
        {
            get 
            {
                Difficulty chosenDifficulty = Settings.ChosenDifficulty;

                if(chosenDifficulty == Difficulty.Dynamic)
                {
                    return DynamicDifficulty;
                }

                return chosenDifficulty;
            }
        }

        public static Difficulty DynamicDifficulty
        {
            get
            {
                switch (CurrentLevelNumber)
                {
                    case 1:
                    case 2:
                    case 3:
                        return Difficulty.Easy;
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        return Difficulty.Normal;
                }

                return Difficulty.Hard;
            }
        }

        public static void LoadRandomPuzzle()
        {
            int chosenPuzzle = RandomIndex(2);
            PuzzleType type = (PuzzleType)chosenPuzzle;
            Puzzle puzzle;

            if (type == PuzzleType.Chess)
            {
                puzzle = new Chess();
                CurrentLevel.ActivatePuzzle(puzzle, PuzzleType.Chess);
                return;
            }

            puzzle = new Sudoku();
            CurrentLevel.ActivatePuzzle(puzzle, PuzzleType.Sudoku);

        }

        public static bool SetUpLevel()
        {
            bool wasLevelGenerated = NewLevel();
            if (wasLevelGenerated)
            {
                PlayerManager.PlayerElement.DidEnterExit = false;
            }

            return wasLevelGenerated;
           
        }

        private static bool NewLevel()
        {
            CurrentLevelValue++;
            if (CurrentLevelValue == 10)
            {
                CurrentLevel = new BossLevel(PlayerManager.PlayerElement);
                return true;
            }

            if (CurrentLevelValue == 11) return false;

            int levelSize = _levelSizes[CurrentLevelValue];
            int levelNumberOfSections = _levelNumberOfSections[CurrentLevelValue];
            MapComposition mapComposition = new MapComposition(_levelCompositions[CurrentLevelValue]);
            CurrentLevel = new Level(levelSize, levelNumberOfSections, mapComposition,PlayerManager.PlayerElement);
            return true;
        }

        public static void ExitLevel()
        {
            SpawnerManager.Instance.Reset();
            EnemyManager.Instance.Reset();

            Printer.LoadingScreen();

            GameManager.StopGameThreads();
            GameManager.PauseGame();

            IsLevelActive = false;
        }

        public static void ResetLevelValue()
        {
            //Change this to 9 to see the boss fight, -1 for normal
            CurrentLevelValue = -1;
        }

  

    }
}
