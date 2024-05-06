

namespace EnemySystem
{
    public class Basilisk
    {
        public CombatEntity CombatEntitiy { get; private set; }

        private int _basiliskSize = 5; //Only works with 5 cause map only places 5 body segments
        private BasiliskSegment[] segments;

        public Basilisk(Point[] positions) 
        {
            segments = new BasiliskSegment[_basiliskSize];

            CombatEntitiy = new CombatEntity(BASILISK_HP,BASILISK_DAMAGE,BASILISK_EVASION,BASILISK_ACCURACY,BASILISK_MULTIHIT,BASILISK_ARMOR,BASILISK_PIERCE);

            segments[0] = new BasiliskHead(this, positions[0]);

            for (int i = 1; i < _basiliskSize - 1; i++)
            {
                segments[i] = new BasiliskBody(this, positions[i]);
            }

            segments[_basiliskSize - 1] = new BasiliskTail(this, positions[_basiliskSize - 1]);
        }

        public void PlaceInSection(Section section)
        {
            section.SectionLayout[3, 2] = segments[0];
            section.SectionLayout[2, 2] = segments[1];
            section.SectionLayout[1, 2] = segments[2];
            section.SectionLayout[1, 1] = segments[3];
            section.SectionLayout[0, 1] = segments[4];
        }

        private BasiliskHead Head
        {
            get
            {
                return (BasiliskHead)segments[0];
            }
        }
        private BasiliskTail Tail
        {
            get
            {
                return (BasiliskTail)segments[4];
            }
        }
        public void Move(Map map,Direction direction)
        {
            Point headPositon = new Point(Head.Position);

            Point positionToMove = headPositon.MovePointInOppositeDirection(direction);
            Direction directionMoved;
            for (int i = 1; i < _basiliskSize; i++)
            {
                directionMoved = segments[i].OnBasiliskMovement(map, positionToMove);
                positionToMove = positionToMove.MovePointInOppositeDirection(directionMoved);
            }
        }

        public bool TailSwipe()
        {
            return Tail.TailSwipe();
        }

        public void Die(Map map)
        {
            Gate.Instance.Open();

            AudioManager.Play(AudioType.BossKill);

            foreach (var segment in segments)
            {
                segment.OnBasiliskDeath(map);
            }
        }


    }
}
