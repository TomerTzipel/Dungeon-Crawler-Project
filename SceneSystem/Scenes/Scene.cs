using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public abstract void SceneLoop();

        protected abstract void HandleInput();

        public abstract void PrintScene();
    }
}
