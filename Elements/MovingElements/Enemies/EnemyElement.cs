
using System;
using System.Diagnostics;
using Utility;

namespace Elements
{
    public abstract class EnemyElement : MovingElement, ITickable
    {

        protected float _movementSpeed;
        protected float _movementCounter = 0;

        protected float _range;

        public CombatEntity CombatEntity { get; protected set; }

        public EnemyElement(Point position, string identifier, CombatEntity combatEntity) : base(position, identifier) 
        {
            CombatEntity = combatEntity;
            EnemyManager.Instance.AddEnemy(this);
        }

        public virtual bool Tick(Map map, float interval)
        {
            _movementCounter += interval;

            if (_movementCounter >= _movementSpeed)
            {
                _movementCounter = 0f;

                Direction direction = CalculateMovement();
                return map.MoveElementInDirection(this,direction);
            }

            return false;
        }

        protected virtual Direction CalculateMovement()
        {
            float distanceFromPlayer = DistanceToPlayer();

            List<Direction> directions = new List<Direction>(4);

            if (distanceFromPlayer <= _range)
            {
                ChooseDirectionsToPlayer(directions);
            }
            else
            {
                directions = [Direction.Down, Direction.Up, Direction.Right, Direction.Left];
            }
            
            return ChooseDirection(directions);
        }
       
        private Direction ChooseDirection(List<Direction> directions)
        {
            int chosenDirectionIndex = RandomIndex(directions.Count);
            Direction chosenDirection = directions[chosenDirectionIndex];

            return chosenDirection;
        }

        private void ChooseDirectionsToPlayer(List<Direction> directions)
        {
            Point playerPosition = new Point (PlayerManager.PlayerElement.Position);
            
            if(playerPosition.X == Position.X)
            {
                if(playerPosition.Y > Position.Y)
                {
                    directions.Add(Direction.Down);
                }
                else
                {
                    directions.Add(Direction.Up);
                }

                return;
            }

            if (playerPosition.Y == Position.Y)
            {
                if (playerPosition.X > Position.X)
                {
                    directions.Add(Direction.Right);
                }
                else
                {
                    directions.Add(Direction.Left);
                }

                return;
            }

            if (playerPosition.X > Position.X)
            {
                directions.Add(Direction.Right);
            }
            else
            {
                directions.Add(Direction.Left);
            }

            if (playerPosition.Y > Position.Y)
            {
                directions.Add(Direction.Down);
            }
            else
            {
                directions.Add(Direction.Up);
            }
        }

        public void GetAttacked(CombatEntity attacker, Map map)
        {
            attacker.Attack(CombatEntity);

            if (!CombatEntity.IsAlive)
            {
                Die(map);
            } 
        }

        protected virtual void Die(Map map)
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

        public override bool CollideWith(MovingElement collidor, Map map)
        {
            if(collidor is PlayerElement player)
            {
                player.GetAttacked(CombatEntity);

                if (!player.CombatEntity.IsAlive)
                {
                    return true;
                }
            }

            return base.CollideWith(collidor,map);
        }

        private float DistanceToPlayer()
        {
            return Point.Distance(Position, PlayerManager.PlayerElement.Position);
        }

        protected abstract void WriteDeathActionText();




    }
}
