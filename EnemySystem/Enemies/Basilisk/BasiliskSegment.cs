

namespace EnemySystem
{
    public abstract class BasiliskSegment : Enemy
    {
        protected Basilisk _basilisk;

        public BasiliskSegment(Basilisk basilisk, Point position, string identifier) : base(position,identifier)
        {
            _basilisk = basilisk;
            CombatEntity = _basilisk.CombatEntitiy;
            Foreground = BASILISK_EFC;
        }

        public override void GetAttacked(CombatEntity attacker, Map map)
        {
            attacker.Attack(CombatEntity);

            if (!CombatEntity.IsAlive)
            {
                _basilisk.Die(map);
            }
        }

        public Direction OnBasiliskMovement(Map map, Point newPosition )
        {
            Direction direction = FindDirectionToMove(newPosition);
            map.MoveElementInDirection(this, direction);

            return direction;
        }

        public void OnBasiliskDeath(Map map)
        {
            Die(map);
        }

        private Direction FindDirectionToMove(Point newPosition)
        {
            if(newPosition.Y == Position.Y)
            {
                if (newPosition.X > Position.X) return Direction.Right;

                return Direction.Left;
            }
            if (newPosition.Y > Position.Y) return Direction.Down;

            return Direction.Up;
        }

        protected override void Die(Map map)
        {
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

    }
}
