
namespace MapElements
{
    public class Trap : WalkableElement
    {
        private bool _isRevealed = false;

        public Trap() : base(TRAP_EI) { }

        public override ConsoleColor Background 
        {
            get 
            {
                if(_isRevealed)
                {
                    return base.Background;
                }

                return EmptyElement.InnerInstance.Background;
                
            }

            protected set
            {
                base.Background = value;
            }
        }

        public override void WalkedOnEffect(MovingElement element)
        {
            if (element is Player player)
            {
                TrapEffect(player);
                _isRevealed = true;
            }
        }

        protected virtual void TrapEffect(Player element) { }

        public override string ToString()
        {
            if (!_isRevealed) 
            {
                return EmptyElement.InnerInstance.ToString();
            }
            return base.ToString();
        }

        public override void TurnBloody() { }
    }
}
