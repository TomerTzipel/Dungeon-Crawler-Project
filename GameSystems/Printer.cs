

namespace GameSystems
{
    public static class Printer
    {
        public static ConsoleColor _currentForeground = DEFAULT_EFC;
        public static ConsoleColor _currentBackground = DEFAULT_EBC;

        private static readonly object _printerLock = new object();


        public static void PrinterSetUp()
        {
            Console.BufferWidth = 20000;
            Console.CursorVisible = false;
            Clear();
        }

        public static void PrintScene(SceneType scene)
        {
            switch (scene)
            {
                case SceneType.MainMenu:
                    PrintMainMenuScene();
                    break;

                case SceneType.Game:
                    PrintGameScene(LevelManager.CurrentLevel);
                    break;

                case SceneType.Inventory:
                    PrintInventoryScene();
                    break;

            }

        }

        public static void PrintLevel()
        {

            lock (_printerLock)
            {
                Console.SetCursorPosition(0, 0);
                LevelManager.CurrentLevel.PrintLevel();
            }

            /* I wrote my own lock and then found out the lock keyword exists. Felt bad deleting it, so I left a memorial.

            bool entered = false;

            while (!entered)
            {
                if (!_isPrinterActive)
                {
                    _isPrinterActive = true;

                    Console.SetCursorPosition(0, 0);
                    LevelManager.CurrentLevel.PrintLevel();

                    _isPrinterActive = false;

                    entered = true;
                }
                else
                {
                    entered = false;
                }
            } 

            May this lock forever stop race conditions. */
        }

        public static void PrintMainMenuScene()
        {
            Clear();
            MainMenu.SetUpMainMenu();
            MainMenu.Print();
        }

        public static void PrintInventoryScene()
        {
            Clear();
        }

        public static void PrintGameScene(Level level)
        {
            Clear();
            PrintLevel();
            PrintMiniMap();
        }

        private static void PrintHUD()
        {

        }

        private static void PrintMiniMap()
        {
            LevelManager.CurrentLevel.PrintMiniMap();
            ColorReset();
        }
        public static void PrintStatus()
        {

        }

        public static void ColorReset()
        {
            Console.ResetColor();
            _currentForeground = DEFAULT_EFC;
            _currentBackground = ConsoleColor.DarkGray;
            Console.ForegroundColor = _currentForeground;
            Console.BackgroundColor = ConsoleColor.DarkGray;
        }
        public static void Clear()
        {
            ColorReset();
            Console.Clear();
        }
        public static void LoadingScreen()
        {
            Clear();
            Console.WriteLine("Loading...");
        }
        public static void CheckColors(Element element)
        {
            if (element.Foreground != _currentForeground)
            {
                _currentForeground = element.Foreground;
                Console.ForegroundColor = _currentForeground;
            }
            if (element.Background != _currentBackground)
            {
                _currentBackground = element.Background;
                Console.BackgroundColor = _currentBackground;
            }
        }

      
    }

}
