﻿

namespace SceneSystem
{
    public class MainMenuScene : SelectionMenuScene
    {
        
        public MainMenuScene() : base(3,"SLAY THE KRAKEN",ConsoleColor.DarkBlue) 
        {
            _buttons[0] = new StartGameButton();
            _buttons[1] = new ReturnToGameButton("Continue Game");
            _buttons[2] = new ChangeSceneButton("Close Game", SceneType.ExitGame);
        }

    }
}