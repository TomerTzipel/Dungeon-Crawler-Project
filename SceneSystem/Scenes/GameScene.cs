using GameSystems;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SceneSystem
{
    public class GameScene : Scene
    {
        public GameScene() : base(new GameInputManager())
        { }

        protected GameInputManager SceneInputManager 
        {
            get 
            { 
                return (GameInputManager)_inputManager; 
            }
        }

        protected override void EnterScene()
        {
            GameManager.ResumeGame();
            SceneManager.PrintCurrentScene();
        }

        public override void SceneLoop()
        {
            if (GameManager.IsShopAvailable)
            {
                GameManager.IsShopAvailable = false;
                SceneManager.ChangeScene(SceneType.Shop);
                return;
            }

            if (GameManager.IsShopActive)
            {
                SceneManager.ChangeScene(SceneType.Shop);
                return;
            }

            if (!LevelManager.IsLevelActive)
            {
                bool wasLevelGenerated = SetUpLevel();
                if (!wasLevelGenerated)
                {
                    GameManager.FinishGame(true);
                    return;
                }
            }

            if (LevelManager.CurrentLevel.IsPuzzleActive)
            {
                SceneManager.ChangeScene(SceneType.Puzzle);
                return;
            }

            EnterScene();

            LevelLoop();

            if (!PlayerManager.CombatEntity.IsAlive)
            {
                GameManager.FinishGame(false);
                LevelManager.ExitLevel();
            }
        }

        private void LevelLoop()
        {
            while (LevelManager.IsLevelActive && 
                   PlayerManager.CombatEntity.IsAlive && 
                   !GameManager.IsGamePaused)
            {
                base.SceneLoop();

                if (PlayerManager.PlayerElement.DidEnterExit)
                {
                    LevelManager.ExitLevel();
                }
            }
            
        }

        protected override void HandleInput()
        {
            InputType inputType = _inputManager.LastInputType;

            switch (inputType)
            {
                case InputType.Movement:
                    HandleMovement();
                    break;

                case InputType.SceneChange:
                    HandleSceneChange();
                    break;

                case InputType.Potion:
                    DrinkPotion();
                    break;

                default:
                    break;
            }
        }


        private void HandleMovement()
        {
            Direction direction = SceneInputManager.TranslateMovementInput();
            LevelManager.CurrentLevel.MovePlayer(PlayerManager.PlayerElement, direction);
            Printer.PrintLevel();
            InputManager.CleanInputBuffer();
        }

        private void HandleSceneChange()
        {
            SceneType scene = SceneInputManager.TranslateSceneChangeInput();

            SceneManager.ChangeScene(scene);

            GameManager.PauseGame();
        }

        private void DrinkPotion()
        {
            PlayerManager.CombatEntity.UsePotion();
            Printer.PrintHUD();
            Printer.PrintActionText();
        }

        private bool SetUpLevel()
        {
            bool wasLevelGenerated = LevelManager.SetUpLevel();

            if (!wasLevelGenerated)
            {
                return false;
            }

            GameManager.StartGameThreads();

            LevelManager.IsLevelActive = true;

            return true;
        }

        public override void PrintScene()
        {
            LevelManager.CurrentLevel.PrintHUD();
            Printer.ColorReset();
      
            LevelManager.CurrentLevel.PrintLevel();
            Printer.ColorReset();

            Printer.PrintActionText();
            Printer.ColorReset();

            LevelManager.CurrentLevel.PrintMiniMap();// Print level only prints mini map after player movement
            Printer.ColorReset();
        }
    }

    

}
