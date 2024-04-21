

using System.Diagnostics;
using System.Xml.Linq;

namespace GameSystems
{
    public static class GameManager
    {
        private static Thread _spawnersThread;
        private static Thread _enemiesThread;
        public static bool IsGamePaused { get; private set; } = true;

        public static bool GameResult{ get; private set; }

        public static void GameSetUp() 
        {
            if (LevelManager.IsLevelActive)
            {
                LevelManager.ExitLevel();
            }

            Printer.ResetActionTextPrinter();
            LevelManager.ResetLevelValue();
            PlayerManager.ClearPlayer();
            PlayerManager.InitializePlayer();
        }

        public static void PauseGame()
        {
            IsGamePaused = true;
        }
        public static void ResumeGame()
        {
            IsGamePaused = false;
        }

        public static void FinishGame(bool result)
        {
            SceneManager.ChangeScene(SceneType.GameOver);
            GameResult = result;
        }
      

        public static void StartGameThreads()
        {
            _spawnersThread = new Thread(new ThreadStart(SpawnerManager.Instance.Start));
            _enemiesThread = new Thread(new ThreadStart(EnemyManager.Instance.Start));
            _spawnersThread.Start();
            _enemiesThread.Start();
        }

        public static void StopGameThreads()
        {
            _spawnersThread.Join();
            _enemiesThread.Join();
        }
    }
}
 