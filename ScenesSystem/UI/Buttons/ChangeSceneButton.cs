

namespace UI_System
{
    public class ChangeSceneButton : Button
    {

        private SceneType _sceneType;

        public ChangeSceneButton(string text, SceneType sceneType) : base(text) 
        {
            _sceneType = sceneType;
        }

        public override bool OnClick()
        {
            SceneManager.ChangeScene(_sceneType);
            return base.OnClick();  
        }


    }
}
