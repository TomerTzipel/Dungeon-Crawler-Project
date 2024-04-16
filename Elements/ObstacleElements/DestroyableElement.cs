

namespace Elements
{
    public class DestroyableElement : ObstacleElement
    {
        protected int _hp;

        protected DestroyableElement(string identifer) : base(identifer) { }

        public DestroyableElement(string identifer, int hp, ConsoleColor foreground) : base(identifer) 
        { 
            _hp = hp;
            Foreground = foreground;
        }
        public override bool HitBy(MovingElement element)
        {

            if (element is PlayerElement)
            {
                _hp--;

                if (_hp == 0)
                {
                    Destroyed();
                    return false;
                }
            }

            return base.HitBy(element);
        }
        protected virtual void Destroyed() { }
        
    }
}
