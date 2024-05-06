
namespace UI_System
{
    public class StartGameButton : ChangeSceneButton
    {
        public StartGameButton() : base("Start Game", SceneType.Game) { }

        public override bool OnClick()
        {
            GameManager.GameSetUp();
            return base.OnClick();
        }
    }
}
