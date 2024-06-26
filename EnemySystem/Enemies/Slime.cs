﻿

namespace EnemySystem
{
    public class Slime : Enemy
    {
        private int _size;

        public Slime(Point position, int size) : base(position, EMPTY_EI)
        {
            EnemyManager.Instance.AddEnemy(this);

            _size = size;
            _movementSpeed = size / 2f;
            _range = SLIME_RANGE;

            Foreground = ConsoleColor.Blue;

            switch (size)
            {
                case STARTING_SLIME_SIZE:
                    Identifier = SLIME3_EI;
                    CombatEntity = new CombatEntity(SLIME3_HP, SLIME3_DAMAGE, SLIME3_EVASION, SLIME_ACCURACY, SLIME3_MULTIHIT,SLIME_ARMOR,SLIME_PIERCE);
                    break;
                case STARTING_SLIME_SIZE-1:
                    Identifier = SLIME2_EI;
                    CombatEntity = new CombatEntity(SLIME2_HP, SLIME2_DAMAGE, SLIME2_EVASION, SLIME_ACCURACY, SLIME2_MULTIHIT, SLIME_ARMOR, SLIME_PIERCE);
                    break;
                case STARTING_SLIME_SIZE - 2:
                    Identifier = SLIME1_EI;
                    CombatEntity = new CombatEntity(SLIME1_HP, SLIME1_DAMAGE, SLIME1_EVASION, SLIME_ACCURACY, SLIME1_MULTIHIT, SLIME_ARMOR, SLIME_PIERCE);
                    break;
            }

            ScaleByDifficulty();
        }


        protected override void Die(Map map)
        {
            EnemyManager.Instance.RemoveEnemy(this);

            Split(map);

            if (IsOnWalkableElement)
            {
                map.AddElement(WalkableElementOnTopOf, Position);
            }
            else
            {
                map.AddElement(EmptyElement.InnerInstance, Position);
            }

            if(_size == STARTING_SLIME_SIZE)
            {
                LootManager.GenerateLoot(LootOrigin.Slime);
            }

        }

        private void Split(Map map)
        {
            if(_size - 1 == STARTING_SLIME_SIZE - 3)
            {
                return;
            }
            AttemptToSpawnChild(map);
            AttemptToSpawnChild(map);
        }

        private void AttemptToSpawnChild(Map map)
        {
            List<Point> spawnsToCheck = [new Point(Position).MovePointInDirection(Direction.Left),
                                         new Point(Position).MovePointInDirection(Direction.Right),
                                         new Point(Position).MovePointInDirection(Direction.Up),
                                         new Point(Position).MovePointInDirection(Direction.Down),
                                         new Point(Position).MovePointInDirection(Direction.Left).MovePointInDirection(Direction.Up),
                                         new Point(Position).MovePointInDirection(Direction.Right).MovePointInDirection(Direction.Up),
                                         new Point(Position).MovePointInDirection(Direction.Left).MovePointInDirection(Direction.Down),
                                         new Point(Position).MovePointInDirection(Direction.Right).MovePointInDirection(Direction.Down)];

            while(spawnsToCheck.Count != 0)
            {
                int indexToCheck = RandomIndex(spawnsToCheck.Count);

                Point pointToCheck = spawnsToCheck[indexToCheck];

                if(map.ElementAt(pointToCheck) is EmptyElement)
                {
                    map.AddElement(new Slime(pointToCheck, _size - 1), pointToCheck);
                    return;
                }
                if (map.ElementAt(pointToCheck) is WalkableElement walkableElement)
                {
                    Slime slime = new Slime(pointToCheck, _size - 1);
                    slime.WalkOn(walkableElement);
                    map.AddElement(slime, pointToCheck);
                    return;
                }

                spawnsToCheck.RemoveAt(indexToCheck);
            }

        }
        protected override void WriteDeathActionText()
        {
            Printer.AddActionText(ActionTextType.CombatPositive, "You killed a slime!");
        }

    }
}
