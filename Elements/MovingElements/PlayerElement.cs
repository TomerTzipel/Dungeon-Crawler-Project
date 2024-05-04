

using GameSystems;
using MapSystems;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elements
{
    public class PlayerElement : MovingElement
    {
        private const int MAX_HP = 1000;
        private const int DAMAGE = 30;
        private const int EVASION = 20;
        private const int ACCURACY = 100;
        private const int MULTI_HIT = 10; 
        private const int ARMOR = 5;
        private const int PIERCE = 10;

        public bool DidEnterExit { get; set; } = false;

        public PlayerCombatEntity CombatEntity { get; protected set; }

        public PlayerElement(Point position) : base(position, PLAYER_RIGHT_EI )
        {
            CombatEntity = new PlayerCombatEntity(MAX_HP, DAMAGE, EVASION, ACCURACY, MULTI_HIT, ARMOR, PIERCE);
            if (Settings.Invincibility)
            {
                CombatEntity = new PlayerCombatEntity(MAX_HP, DAMAGE, 100, ACCURACY, MULTI_HIT, ARMOR, PIERCE);
            }

            IsOnWalkableElement = true;
            ChangeColor(Settings.PlayerColor);
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
            if (Settings.Invincibility) return;
           
            int damage = CombatEntity.LoseHpByMaxHpPrecentage(precentage);
            Printer.AddActionText(ActionTextType.CombatNegative, $"YOU STEPPED ON SPIKES! YOU LOST {damage} HP");
        }

        public void ChangeColor(ConsoleColor color)
        {
            Foreground = color;
        }
    }
}
