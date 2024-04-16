

namespace Elements
{
    public class WalkableElement : Element
    {
        public WalkableElement(string identifier) : base(identifier) { }

        public WalkableElement(string identifier, ConsoleColor foreground) : base(identifier) 
        { 
            Foreground = foreground;
        }
        public WalkableElement(ConsoleColor background,string identifier) : base(identifier)
        {
            Background = background;
        }
        public virtual void WalkedOnEffect(MovingElement element){}

    }
}
