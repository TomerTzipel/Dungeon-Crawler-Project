

namespace Elements
{
    internal class ExitElement : WalkableElement
    {
        public ExitElement() : base(EXIT_EI) { }
        public override void WalkedOnEffect(MovingElement element) 
        {
            if(element is PlayerElement player)
            {
                //Add a check for Exit Key in Inventory
                player.DidEnterExit = true;
            }
        }
    }
}
