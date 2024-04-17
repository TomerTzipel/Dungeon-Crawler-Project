

using GameSystems;
using MapSystems;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elements
{
    public class PlayerElement : MovingElement
    {
        public bool DidEnterExit { get; set; } = false;

        public PlayerCombatEntity CombatEntity { get; protected set; }

        public PlayerElement(Point position) : base(position, PLAYER_RIGHT_EI )
        {
            CombatEntity = new PlayerCombatEntity(50, 15, 20, 80, 10,5,10);
            IsOnWalkableElement = true;
        }

        public Inventory Inventory
        {
            get { return CombatEntity.Inventory; }
        }

        public void InitializePositionOnNewLevel(Point position)
        {
            Position = position;
        }

        public void ChangeDirection(Direction direction)
        {
            if (direction == Direction.Left)
            {
                Identifier = PLAYER_LEFT_EI;
            }
            if (direction == Direction.Right)
            {
                Identifier = PLAYER_RIGHT_EI;
            }
        }

        public bool GetAttacked(CombatEntity attacker)
        {
            return attacker.Attack(CombatEntity);
        }

        public override bool CollideWith(MovingElement collidor, Map map)
        {
            if (collidor is EnemyElement enemy)
            {
                enemy.GetAttacked(CombatEntity,map);

                if (!enemy.CombatEntity.IsAlive)
                {
                    return true;
                }
            }

            return base.CollideWith(collidor, map);
        }

        public void LoseHpByMaxHpPrecentage(int precentage)
        {
            CombatEntity.LoseHpByMaxHpPrecentage(precentage);
        }

        
    }
}
