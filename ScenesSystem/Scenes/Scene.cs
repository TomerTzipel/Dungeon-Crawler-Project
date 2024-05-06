
namespace SceneSystem
{
  
    public abstract class Scene
    {
        protected InputManager _inputManager;

        public Scene(InputManager inputManager)
        {
            _inputManager = inputManager;
        }
        protected abstract void EnterScene();
        public virtual void SceneLoop()
        {
            bool wasInputDetected = _inputManager.ReadInput();

            if (wasInputDetected)
            {
                HandleInput();
            }
        }

        protected abstract void HandleInput();

        public abstract void PrintScene();
    }
}
