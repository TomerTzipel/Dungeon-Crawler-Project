

namespace GameSystems
{
    public static class Settings
    {
        public static Difficulty ChosenDifficulty = Difficulty.Dynamic;

        public static ConsoleColor PlayerColor = DEFAULT_EFC;

        public static bool Invincibility = false;

        public static bool RevealMiniMap = false;

        public static void changeDifficulty(Difficulty difficulty)
        {
            ChosenDifficulty = difficulty;
        }
        public static void ToggleRevealMiniMap()
        {
            RevealMiniMap = !RevealMiniMap;
        }
        public static void ToggleInvincibility()
        {
            Invincibility = !Invincibility;
        }
        public static void ChangePlayerColor(ConsoleColor color)
        {
            PlayerColor = color;

            if (PlayerManager.IsPlayerInit)
            {
                PlayerManager.PlayerElement.ChangeColor(color);
            }
        }

    }
}
