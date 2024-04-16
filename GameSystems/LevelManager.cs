

namespace GameSystems
{
    public static class LevelManager
    {
        public static Level CurrentLevel { get; private set; }

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
        public static int CurrentLevelValue { get; private set; } = -1;

        public static int CurrentLevelNumber
        {
            get { return CurrentLevelValue + 1; }
        }

        public static void LoadRandomPuzzle(Puzzle puzzle)
        {
            CurrentLevel.ActivatePuzzle(puzzle);
            Printer.Clear();
        }

        public static void SetUpLevel()
        {
            NewLevel();
            Printer.ResetLevel(CurrentLevel);
            PlayerManager.PlayerElement.DidEnterExit = false;
        }

        private static void NewLevel()
        {
            CurrentLevelValue++;

            int levelSize = _levelSizes[CurrentLevelValue];
            int levelNumberOfSections = _levelNumberOfSections[CurrentLevelValue];
            MapComposition mapComposition = new MapComposition(_levelCompositions[CurrentLevelValue]);
            CurrentLevel = new Level(levelSize, levelNumberOfSections, mapComposition,PlayerManager.PlayerElement);
        }
    }
}
