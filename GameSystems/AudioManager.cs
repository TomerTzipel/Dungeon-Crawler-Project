

namespace GameSystems
{
    public enum AudioType
    {
        GameLose, GameWin, Spawn
    }

    public static class AudioManager
    {
        public static void Play(AudioType type)
        {
            Thread _audioThread = null;

            switch (type)
            {
                case AudioType.GameLose:
                    _audioThread = new Thread(new ThreadStart(PlayGameLose));
                    break;
                case AudioType.GameWin:
                    _audioThread = new Thread(new ThreadStart(PlayGameWin));
                    break;
                case AudioType.Spawn:
                    _audioThread = new Thread(new ThreadStart(PlaySpawn));
                    break;
            }

            if(_audioThread != null)
            {
                _audioThread.Start();
            }
            
        }
        private static void PlaySpawn()
        {
            Console.Beep();
        }
        private static void PlayGameWin()
        {
            Console.Beep();
            Console.Beep();
            Console.Beep();
            Console.Beep();
            Console.Beep();
        }
        private static void PlayGameLose()
        {
            Console.Beep();
            Console.Beep();
            Console.Beep();
        }

    }
}
