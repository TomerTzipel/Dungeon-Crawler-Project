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
            

        public override void SceneLoop()
        {
            if(!LevelManager.IsLevelActive)
            {
                bool wasLevelGenerated = SetUpLevel();
                if(!wasLevelGenerated) 
                {
                    GameManager.FinishGame(true);
                    return;
                }
            }

            LevelLoop();

            if (!PlayerManager.PlayerElement.CombatEntity.IsAlive)
            {
                GameManager.FinishGame(false);
                LevelManager.ExitLevel();
            }
        }

        private void LevelLoop()
        {
            while (LevelManager.IsLevelActive && 
                   PlayerManager.PlayerElement.CombatEntity.IsAlive && 
                   !GameManager.IsGamePaused)
            {
                bool wasInputDetected = _inputManager.ReadInput();

                if (wasInputDetected)
                {
                    HandleInput();
                }

                if (PlayerManager.PlayerElement.DidEnterExit)
                {
                    LevelManager.ExitLevel();
                }
            }
            
        }

        protected override void HandleInput()
        {
            ConsoleKeyInfo input = _inputManager.LastInput;
            InputType inputType = _inputManager.LastInputType;

            switch (inputType)
            {
                case InputType.Movement:
                    HandleMovement();
                    break;

                case InputType.Sudoku:
                    HandleSudoku();
                    break;

                case InputType.SceneChange:
                    HandleSceneChange();
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
        private void HandleSudoku()
        {
            Level level = LevelManager.CurrentLevel;

            if (level.IsPuzzleActive && level.Puzzle is SudokuPuzzle puzzle)
            {
                int value = SceneInputManager.TranslateSudokuInput();
                if (value < 0)
                {
                    level.DeActivatePuzzle();
                    return;
                }

                bool wasSolved = puzzle.InputValue(value);
                if (wasSolved)
                {
                    //generate reward
                    level.DeActivatePuzzle();
                    return;
                }
            }
            Printer.PrintLevel();
            InputManager.CleanInputBuffer();
        }

        private void HandleSceneChange()
        {
            SceneType scene = SceneInputManager.TranslateSceneChangeInput();

            SceneManager.ChangeScene(scene);

            GameManager.PauseGame();

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
            GameManager.ResumeGame();

            return true;
        }

       

        public override void PrintScene()
        {
            
        }
    }

    

}
