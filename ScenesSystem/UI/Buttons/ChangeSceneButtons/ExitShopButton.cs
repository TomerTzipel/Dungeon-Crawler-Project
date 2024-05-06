

namespace UI_System
{
    public class ExitShopButton : ChangeSceneButton
    {
        public ExitShopButton() : base("Exit Shop",SceneType.Game)
        { }

        public override bool OnClick()
        {
            GameManager.IsShopActive = false;
            return base.OnClick();
        }
    }
}
