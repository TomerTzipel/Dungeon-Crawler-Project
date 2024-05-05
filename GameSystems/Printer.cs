

using System.Data;

namespace GameSystems
{
    public static class Printer
    {
        public static ConsoleColor _currentForeground = DEFAULT_EFC;
        public static ConsoleColor _currentBackground = DEFAULT_EBC;

       

        public static readonly Point PRINTER_PIVOT = new Point(0, 0);

        private static readonly object _printerLock = new object();
       

        private const int ACTION_TEXT_MAX_LINES = 10;
        public static ActionTextPrinter ActionTextPrinter { get; private set; }

        public static Camera Camera { get; private set; }

        public const int CAMERA_WIDTH = 21;
        private const int CAMERA_HEIGHT = 13;

        public static void SetUp()
        {
            Camera = new Camera(CAMERA_WIDTH,CAMERA_HEIGHT);

            //Console.BufferWidth = 5000;
            ResetActionTextPrinter();
            Console.CursorVisible = false;
            Clear();
        }

        public static void PrintScene(Scene scene)
        {
            lock (_printerLock)
            {
                //SetPrinterPosition();
                Console.SetCursorPosition(0, 0);
                scene.PrintScene();
            }

        }

        public static void PrintLevel()
        {

            lock (_printerLock)
            {
                //SetPrinterPosition();
                Console.SetCursorPosition(0, 0);
                LevelManager.CurrentLevel.PrintLevel();

                if (PlayerManager.CombatEntity.DoesHUDNeedReprint) PrintHUD();

                if (ActionTextPrinter.DoesNeedReprint) PrintActionText();

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

        public static void PrintHUD()
        {
            lock (_printerLock)
            {
                //SetPrinterPosition();
                Console.SetCursorPosition(0, 0); 
                ColorReset();
                LevelManager.CurrentLevel.PrintHUD();
            }

        }

        public static void PrintActionText()
        {
            lock (_printerLock)
            {
                //SetPrinterPosition();
                Console.SetCursorPosition(0, 5 + CAMERA_HEIGHT + 1);
                ColorReset();
                ActionTextPrinter.Print();
                ColorReset(); 
            }
        }

        public static void AddActionText(ActionTextType type,string text)
        {
            ActionText line = new ActionText(type, text);
            ActionTextPrinter.AddLine(line);
        }

        public static void ColorReset()
        {
            Console.ResetColor();
            _currentForeground = DEFAULT_EFC;
            _currentBackground = ConsoleColor.DarkGray;
            Console.ForegroundColor = _currentForeground;
            Console.BackgroundColor = _currentBackground;
        }

        public static void Clear()
        {
            lock (_printerLock)
            {
                ColorReset();
                Console.Clear();
            }
               
        }

        public static void LoadingScreen()
        {
            lock (_printerLock)
            {
                Clear();
                Console.WriteLine("Loading...");
            }        
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

        public static void SetPrinterPosition()
        {
            SetPrinterPosition(0,0);
        }

        public static void SetPrinterPosition(int row, int column)
        {
            Console.SetCursorPosition(PRINTER_PIVOT.X + column, PRINTER_PIVOT.Y + row);
        }

        public static void ResetActionTextPrinter()
        {
            ActionTextPrinter = new ActionTextPrinter(ACTION_TEXT_MAX_LINES);
        }
    }

}
