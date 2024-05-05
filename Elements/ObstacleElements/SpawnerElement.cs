
using System.Diagnostics;


namespace Elements
{
    public class SpawnerElement : DestroyableElement, ITickable
    {

        private float _spawnRate = 10f;
        private float _spawnCounter = 0;

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
               _spawnCounter = 0;
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

            //FIX change to a function that decides on the enemy to spawn
            Bat bat = new Bat(spawnPosition);
            map.AddElement(bat, spawnPosition);
            EnemyManager.Instance.AddEnemy(bat);

            return true;
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
