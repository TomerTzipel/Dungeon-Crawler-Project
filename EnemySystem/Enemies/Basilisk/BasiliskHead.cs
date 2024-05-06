

namespace EnemySystem
{
    public class BasiliskHead : BasiliskSegment
    {
        private bool _wasPlayerHit = false;

        public BasiliskHead(Basilisk basilisk, Point position) : base(basilisk, position, BASILISK_HEAD_EI)
        {
            _movementSpeed = BASILISK_MOVEMENT_SPEED;
            ScaleByDifficulty();
            EnemyManager.Instance.AddEnemy(this); 
        }

        public override bool Tick(Map map, float interval)
        {
            _movementCounter += interval;
            
            if (_movementCounter >= _movementSpeed)
            { 
                _movementCounter = 0f;
                return AttemptToMove(map);
            }

            return false; 
        }

        private bool AttemptToMove(Map map)
        {
            
            if (_basilisk.TailSwipe())
            {
                return false;
            }

            List<Direction> directions = [Direction.Left, Direction.Right, Direction.Up, Direction.Down];
            List<Direction> directionsToPlayer = new List<Direction>(4);
            FindDirectionsToPlayer(directionsToPlayer);

            foreach (Direction direction in directionsToPlayer) { directions.Remove(direction); }

            bool movementResult;

            movementResult = AttemptToMoveInDirections(map, directionsToPlayer);

            if (movementResult)
            {
                return true;
            }
            movementResult = AttemptToMoveInDirections(map, directions);

            if (movementResult)
            {
                return true;
            }
            return false;

        }

        private bool AttemptToMoveInDirections(Map map, List<Direction> directions)
        {
            while (directions.Count != 0)
            {
                Direction direction = ChooseDirection(directions);
                bool movementResult = AttemptToMoveInDirection(map, direction);

                if (movementResult || _wasPlayerHit)
                {
                    _wasPlayerHit = false;
                    return true;
                }

                directions.Remove(direction);
            }

            return false;
        }
        private bool AttemptToMoveInDirection(Map map, Direction direction)
        {
            bool movementResult = map.MoveElementInDirection(this, direction);

            if (movementResult)
            {
                _basilisk.Move(map, direction);
            }

            return movementResult;
        }

        protected override void Die(Map map)
        {
            EnemyManager.Instance.RemoveEnemy(this);
            WriteDeathActionText();
            if (IsOnWalkableElement)
            {
                WalkableElementOnTopOf.TurnBloody();
                map.AddElement(WalkableElementOnTopOf, Position); 
            }
            else
            {
                map.AddElement(new WalkableElement(BLOOD_PUDDLE_EBC, EMPTY_EI), Position);
            }
        }

        public override bool CollideWith(Element collidor, Map map)
        {
            if (collidor is Player player)
            {
                _wasPlayerHit = true;
            }

            return base.CollideWith(collidor, map);
        }

        protected override void WriteDeathActionText()
        {
            Printer.AddActionText(ActionTextType.Victory, "You Slayed The Basilisk!!!!");
        }
    }
}
