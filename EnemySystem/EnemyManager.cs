
namespace EnemySystem
{
    public class EnemyManager : Ticker
    {
        private static EnemyManager _instance;
        private static bool _isInitialized = false;

        private EnemyManager() { }

        public static EnemyManager Instance
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
            _instance = new EnemyManager();
            _isInitialized = true;
        }

        public void AddEnemy(Enemy enemy)
        {
           AddTickable(enemy);  
        }

        public void RemoveEnemy(Enemy enemy)
        {
            RemoveTickable(enemy);
        }

        public override void Start()
        {
            IsActive = true;

            while (IsActive && (!(_tickables.Count == 0) || SpawnerManager.Instance.IsActive))
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
