
using System.Diagnostics;


namespace Elements
{
    public class SpawnerElement : DestroyableElement, ITickable
    {

        private float _spawnRate = 20f;
        private float _spawnCounter = 15f;

        private Point _position;
 
        public SpawnerElement(Point position) : base(SPAWNER_EI)
        {
            _hp = RandomRange(20, 30);
            _position = position;

            SpawnerManager.Instance.AddSpawner(this);
        }

        public bool Tick(Map map, float interval)
        {
            _spawnCounter += interval;

            if (_spawnCounter >= _spawnRate) 
            {
               _spawnCounter = 0f;
               return Spawn(map);
            }

            return false;
        }

        private bool Spawn(Map map)
        {
            Point spawnPosition = FindAvailableSpawnPosition(map);

            if(spawnPosition.X == -1)
            {
                return false;
            }

            EnemyElement enemy = ChooseEnemyToSpawn(spawnPosition);
            
            map.AddElement(enemy, spawnPosition);
            EnemyManager.Instance.AddEnemy(enemy);

            return true;
        }

        private EnemyElement ChooseEnemyToSpawn(Point spawnPosition)
        {

            int chosenEnemy = RandomIndex(3);
            EnemyType enemyType= (EnemyType)chosenEnemy;

            EnemyElement enemyToSpawn = null;

            switch (enemyType)
            {
                case EnemyType.Bat:
                    enemyToSpawn = new Bat(spawnPosition);
                    break;

                case EnemyType.Slime:
                    enemyToSpawn = new Slime(spawnPosition, STARTING_SLIME_SIZE);
                    break;

                case EnemyType.Ogre:
                    enemyToSpawn = new Ogre(spawnPosition);
                    break;
            }

            return enemyToSpawn;

        }

        private Point FindAvailableSpawnPosition(Map map)
        {
            int chosenDirectionIndex;
            
            Direction chosenDirection;
            List<Direction> directions = [Direction.Up, Direction.Down, Direction.Left, Direction.Right];
            while (directions.Count > 0)
            {
                chosenDirectionIndex = RandomIndex(directions.Count);
                chosenDirection = directions[chosenDirectionIndex];

                Point positionToCheck = new Point(_position);
                positionToCheck.MovePointInDirection(chosenDirection);

                if(map.ElementAt(positionToCheck) is EmptyElement)
                {
                    return positionToCheck;
                }

                directions.Remove(chosenDirection);
            }

            return new Point(-1,-1);
        }

        protected override void Destroyed()
        {
            SpawnerManager.Instance.RemoveSpawner(this);
        }
    }
}
