using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_System
{
    public class ReturnToGameButton : ChangeSceneButton
    {
        public ReturnToGameButton(string text) : base(text, SceneType.Game)
        { }

        public override bool OnClick()
        {
            if (!LevelManager.IsLevelActive) return false;


            GameManager.ResumeGame();
            return base.OnClick();
        }
    }
}
