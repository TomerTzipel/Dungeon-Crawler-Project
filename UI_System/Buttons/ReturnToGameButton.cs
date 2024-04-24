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
            if(GameManager.IsShopActive) return base.OnClick();

            if (!LevelManager.IsLevelActive ) return false;

            return base.OnClick();
        }
    }
}
