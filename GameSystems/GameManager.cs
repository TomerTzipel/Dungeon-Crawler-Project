

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

        //True if player is still alive, false otherwise
        public static bool GameLoop()
        {
            LevelManager.SetUpLevel();
 
            Thread spawnersThread = new Thread(new ThreadStart(SpawnerManager.Instance.Start));
            spawnersThread.Start();

            Thread enemiesThread = new Thread(new ThreadStart(EnemyManager.Instance.Start));
            enemiesThread.Start();

            while (PlayerManager.PlayerElement.CombatEntity.IsAlive)
            {
                InputType input = GameInputManagerOld.ReadInput();

                switch (input)
                {
                    case InputType.Movement:
                        GameInputManagerOld.HandleMovementInput(LevelManager.CurrentLevel);
                        break;

                    case InputType.Sudoku:
                        GameInputManagerOld.HandleSudokuInput(LevelManager.CurrentLevel);
                        break;

                    case InputType.SceneChange:
                        GameInputManagerOld.HandleSceneChnageInput();
                        break;

                    case InputType.MenuMovement:
                        GameInputManagerOld.HandleMenuInput();
                        break;

                    case InputType.Error:
                        continue;

                }

                if (PlayerManager.PlayerElement.DidEnterExit)
                {
                    break;
                }      
            }

            SpawnerManager.Instance.Reset();
            EnemyManager.Instance.Reset();

            Printer.LoadingScreen();

            spawnersThread.Join();
            enemiesThread.Join();

            if (PlayerManager.PlayerElement.CombatEntity.IsAlive) return true;

            return false;

        }

    }
}
 