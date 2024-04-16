

using System.Diagnostics;
using System.Xml.Linq;

namespace GameSystems
{
    public static class GameManager
    {

        public static bool IsGamePaused { get; private set; } = false;

        public static void GameSetUp() 
        {
            Console.BufferWidth = 20000;
            Console.CursorVisible = false;
            PlayerManager.InitializePlayer();
            Printer.Clear();
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
                InputType input = InputManager.ReadInput();

                switch (input)
                {
                    case InputType.Movement:
                        InputManager.HandleMovementInput(LevelManager.CurrentLevel);
                        break;

                    case InputType.Sudoku:
                        InputManager.HandleSudokuInput(LevelManager.CurrentLevel);
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

        public static void PauseGame()
        {
            IsGamePaused = true;
        }
        public static void ResumeGame()
        {
            IsGamePaused = false;
        }

        public static void GameOver() 
        { 
            IsGamePaused = true;
        }

    }
}
 