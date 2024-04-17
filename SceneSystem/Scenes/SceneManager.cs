

namespace SceneSystem
{
    public enum SceneType
    {
        MainMenu, Game, Inventory, GameOver, PauseMenu,
        ExitGame,
        
    }
    public static class SceneManager
    {
        private static Scene[] _scenes = new Scene[5];

        private static SceneType _currentScene = SceneType.MainMenu;


        public static void SetUpScenes()
        {
            _scenes[(int)SceneType.MainMenu] = new MainMenuScene();
            _scenes[(int)SceneType.Game] = new GameScene();
            _scenes[(int)SceneType.Inventory] = new InventoryMenuScene();
            _scenes[(int)SceneType.GameOver] = new GameOverMenuScene();
            _scenes[(int)SceneType.PauseMenu] = new PauseMenuScene();
        }

        public static void RunGame()
        {
            while (_currentScene != SceneType.ExitGame) 
            {
                
                int scene = (int)_currentScene;

                _scenes[scene].SceneLoop(); //The loop is left only on scene change

                Printer.Clear();
            }
        }

        public static void ChangeScene(SceneType newScene)
        {
            _currentScene = newScene;
        }
    }
}
