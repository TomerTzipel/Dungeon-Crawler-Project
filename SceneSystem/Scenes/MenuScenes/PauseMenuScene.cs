using Dungeon_Crawler.SceneSystem.InputManagers;
using InventorySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneSystem
{
    public class PauseMenuScene : SelectionMenuScene
    {
        public PauseMenuScene() : base(2,"GAME IS PAUSED",ConsoleColor.White) 
        { 
            _inputManager = new PauseMenuInputManager();

            _buttons[0] = new ReturnToGameButton("Resume"); 
            _buttons[1] = new ChangeSceneButton("Main Menu", SceneType.MainMenu);
        }

        protected override void HandleInput()
        {
            InputType inputType = _inputManager.LastInputType;

            if(inputType == InputType.SceneChange)
            {
                ActivateButton(_buttons[0]);
                return;
            }

            base.HandleInput();
        }

        public override void PrintScene()
        {
            base.PrintScene();

            Console.SetCursorPosition(25, 0);
            PlayerManager.CombatEntity.PrintPlayerStats();
        }

    }
}
