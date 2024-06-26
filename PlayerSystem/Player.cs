﻿

namespace PlayerSystem
{
    public class Player : MovingElement
    {
        private const int MAX_HP = 300;
        private const int DAMAGE = 30;
        private const int EVASION = 10;
        private const int ACCURACY = 85;
        private const int MULTI_HIT = 10; 
        private const int ARMOR = 5;
        private const int PIERCE = 5;

        public bool DidEnterExit { get; set; } = false;

        public PlayerCombatEntity CombatEntity { get; protected set; }

        public Player(Point position) : base(position, PLAYER_RIGHT_EI )
        {
            CombatEntity = new PlayerCombatEntity(MAX_HP, DAMAGE, EVASION, ACCURACY, MULTI_HIT, ARMOR, PIERCE);

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
                switch (Identifier)
                {
                    case (PLAYER_LEFT_EI):
                    case (PLAYER_ATTACK_LEFT_EI):
                        break;

                    case (PLAYER_RIGHT_EI):
                        Identifier = PLAYER_LEFT_EI;
                        break; 

                    case (PLAYER_ATTACK_RIGHT_EI):
                        Identifier = PLAYER_ATTACK_LEFT_EI;
                        break;
                }
            }

            if (direction == Direction.Right)
            {
                switch (Identifier)
                {
                    case (PLAYER_RIGHT_EI):
                    case (PLAYER_ATTACK_RIGHT_EI):
                        break;

                    case (PLAYER_LEFT_EI):
                        Identifier = PLAYER_RIGHT_EI;
                        break;

                    case (PLAYER_ATTACK_LEFT_EI):
                        Identifier = PLAYER_ATTACK_RIGHT_EI;
                        break;

                    

                }
            }
        }

        public bool GetAttacked(CombatEntity attacker)
        {
            return attacker.Attack(CombatEntity);
        }

        public override bool CollideWith(Element collidor, Map map)
        {
            

            if (collidor is Vase || collidor is Boulder || collidor is Bush || collidor is Spawner)
            {
                AttackAnimation();
                AudioManager.Play(AudioType.DestroyableHit);
                return false;
            }

            if (collidor is Enemy enemy)
            {
                enemy.GetAttacked(CombatEntity,map);

                AudioManager.Play(AudioType.Attack);
                AttackAnimation();

                if (!enemy.CombatEntity.IsAlive)
                {
                    return true;
                }
            }

            if (collidor is ObstacleElement && !(collidor is DestroyableElement))
            {
                AudioManager.Play(AudioType.ObstacleHit);
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

        private void AttackAnimation()
        {
            switch (Identifier)
            {
                case(PLAYER_LEFT_EI):
                    Identifier = PLAYER_ATTACK_LEFT_EI;
                    break;

                case (PLAYER_RIGHT_EI):
                    Identifier = PLAYER_ATTACK_RIGHT_EI;
                    break;

                case (PLAYER_ATTACK_LEFT_EI):
                    Identifier = PLAYER_LEFT_EI;
                    break;

                case (PLAYER_ATTACK_RIGHT_EI):
                    Identifier = PLAYER_RIGHT_EI;
                    break;
            }
        }



    }
}
