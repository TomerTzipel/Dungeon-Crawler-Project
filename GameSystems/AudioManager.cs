

using System;

namespace GameSystems
{
    public enum AudioType
    {
        GameLose, GameWin, Spawn, PuzzleSolved, ShopOpen, ObstacleHit, DestroyableHit, Attack , BossKill
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
                case AudioType.PuzzleSolved:
                    _audioThread = new Thread(new ThreadStart(PlayPuzzleSolve));
                    break;
                case AudioType.ShopOpen:
                    _audioThread = new Thread(new ThreadStart(PlayShopOpen));
                    break;
                case AudioType.ObstacleHit:
                    _audioThread = new Thread(new ThreadStart(PlayObstacleHit));
                    break;
                case AudioType.DestroyableHit:
                    _audioThread = new Thread(new ThreadStart(PlayDestroyableHit));
                    break;
                case AudioType.Attack:
                    _audioThread = new Thread(new ThreadStart(PlayAttack));
                    break;
                case AudioType.BossKill:
                    _audioThread = new Thread(new ThreadStart(PlayBossKill));
                    break;
            }

            if(_audioThread != null)
            {
                _audioThread.Start();
            }
            
        }
        private static void PlaySpawn()
        {
            Console.Beep(500, 150);
        }
        private static void PlayGameWin()
        {
            Console.Beep(800, 250);
            Console.Beep(900, 250);
            Console.Beep(1000, 250);
        }
        private static void PlayGameLose()
        {
            Console.Beep(100, 250);
            Console.Beep(80, 250);
            Console.Beep(60, 250);
        }
        private static void PlayPuzzleSolve()
        {
            Console.Beep(369, 200);
            Console.Beep(369, 200);
            Console.Beep(369, 200);
            Console.Beep(293, 200);
            Console.Beep(246, 200);
            Console.Beep(329, 200);
            Console.Beep(329, 200);
            Console.Beep(329, 200);
            Console.Beep(415, 200);
            Console.Beep(415, 200);
            Console.Beep(440, 200);
            Console.Beep(493, 200);
            Console.Beep(440, 200);

        }
        private static void PlayShopOpen()
        {
            Console.Beep(704, 750);
            Console.Beep(792, 250);
            Console.Beep(880, 500);
            Console.Beep(792, 500);
            Console.Beep(940, 500);
            Console.Beep(880, 500);
            Console.Beep(792, 500);

        }
        private static void PlayObstacleHit()
        {
            Console.Beep(60, 150);
        }
        private static void PlayDestroyableHit()
        {
            Console.Beep(150, 150);
        }
        private static void PlayAttack()
        {
            Console.Beep(300, 150);
        }
        private static void PlayBossKill()
        {
            Console.Beep(1320, 1500); 
        }
    }
}
