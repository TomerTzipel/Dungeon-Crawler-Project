

namespace Elements
{
    public abstract class MovingElement : Element
    {
        protected ConsoleColor _originalBackgroundColor;

        public Point Position { get; protected set; }

        public bool IsStunned { get; private set; } = false;
        private int _stunCounter;
        private int _stunImmunityCounter;
        private Direction _lastDirection;

        public WalkableElement WalkableElementOnTopOf { get; protected set; }
        public bool IsOnWalkableElement { get; protected set; } = false;

        public MovingElement(Point position, string identifier) : base(identifier)
        {
            Position = position;
        }

        public void WalkOn(WalkableElement element)
        {
            WalkableElementOnTopOf = element;
            IsOnWalkableElement = true;
            _originalBackgroundColor = Background;

            element.WalkedOnEffect(this);
            
            Background = element.Background;

            

           
        }

        public void WalkOff()
        {
            WalkableElementOnTopOf = null;
            IsOnWalkableElement = false;
            Background = _originalBackgroundColor;
        }

        public void Move(Direction direction)
        {
            _stunImmunityCounter--;
            Position.MovePointInDirection(direction);
        }

        public void Stun(int stunValue)
        {
            if(_stunImmunityCounter <= 0) 
            {
                IsStunned = true;
                _stunCounter = stunValue;
                _lastDirection = Direction.Error; // For the first movement to count no matter the direction
            }
        }

        public void ProgressStunStatus(Direction direction)
        {
            if (_lastDirection != direction)
            {
                _stunCounter--;

                if (_stunCounter == 0)
                {
                    IsStunned = false;
                    _stunImmunityCounter = 5;
                }
            }

            _lastDirection = direction;
        }

        //Return false if movement isn't allowed
        public virtual bool CollideWith(MovingElement collidor, Map map)
        {
            return false;
        }

    }
}
