

namespace SceneSystem
{
    public enum SceneType
    {
        MainMenu, Game, Inventory, GameOver, PauseMenu, Puzzle, SudokuPuzzle, Shop, Settings, ChessPuzzle,
        ExitGame
        
    }
    public static class SceneManager
    {
        private static Scene[] _scenes = new Scene[10];

        private static SceneType _currentScene = SceneType.MainMenu;


        public static void SetUp()
        {
            _scenes[(int)SceneType.MainMenu] = new MainMenuScene();
            _scenes[(int)SceneType.Game] = new GameScene();
            _scenes[(int)SceneType.Inventory] = new InventoryScene();
            _scenes[(int)SceneType.GameOver] = new GameOverMenuScene();
            _scenes[(int)SceneType.PauseMenu] = new PauseMenuScene();
            _scenes[(int)SceneType.Puzzle] = new PuzzleScene();
            _scenes[(int)SceneType.SudokuPuzzle] = new SudokuScene();
            _scenes[(int)SceneType.Shop] = new ShopScene();
            _scenes[(int)SceneType.Settings] = new SettingsScene();
            _scenes[(int)SceneType.ChessPuzzle] = new ChessScene();
        }

        public static void RunGame()
        {
            while (_currentScene != SceneType.ExitGame) 
            {
                
                int scene = (int)_currentScene;

                _scenes[scene].SceneLoop(); //The loop is left only on scene change

                InputManager.CleanInputBuffer();
                Printer.Clear();
            }

            if (LevelManager.IsLevelActive)
            {
                LevelManager.ExitLevel();
                Printer.Clear();
            }
        }

        public static void ChangeScene(SceneType newScene)
        {
            _currentScene = newScene;
        }

        public static void PrintCurrentScene()
        {
            Printer.PrintScene(_scenes[(int)_currentScene]);
        }
    }
}
