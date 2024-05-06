

namespace EnemySystem
{
    public class SpawnerManager : Ticker
    {
        private static SpawnerManager _instance;
        private static bool _isInitialized = false;

        private SpawnerManager() { }

        public static SpawnerManager Instance
        {
            get
            {
                if (!_isInitialized)
                {
                    Initialize();
                }

                return _instance;
            }
        }

        private static void Initialize()
        {
            _instance = new SpawnerManager();
            _isInitialized = true;
        }

        public void AddSpawner(Spawner spawner)
        {
            AddTickable(spawner);
        }

        public void RemoveSpawner(Spawner spawner)
        {
            RemoveTickable(spawner);
        }

        public override void Start()
        {
            IsActive = true;

            while (IsActive && !(_tickables.Count == 0))
            {
                _tickStopwatch.Stop();

                if (!GameManager.IsGamePaused)
                {
                    Tick((float)_tickStopwatch.Elapsed.TotalSeconds);
                }

                Thread.Sleep(100);
            }

            IsActive = false;
        }
    }
}


    


