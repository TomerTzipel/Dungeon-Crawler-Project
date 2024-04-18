using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneSystem
{
    public class InventoryMenuScene : Scene
    {

        public InventoryMenuScene() : base(new InventoryMenuInputManger()) { }

        protected override void EnterScene()
        {
            throw new NotImplementedException();
        }

        public override void SceneLoop()
        {
            throw new NotImplementedException();
        }
      
        protected override void HandleInput()
        {
            throw new NotImplementedException();
        }
        public override void PrintScene()
        {
            throw new NotImplementedException();
        }
    }
}
