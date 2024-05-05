

namespace GameSystems
{
    public static class Settings
    {
        public static Difficulty ChosenDifficulty = Difficulty.Dynamic;

        public static ConsoleColor PlayerColor = DEFAULT_EFC;

        public static bool Invincibility { get; private set; } = false;

        public static bool RevealMiniMap { get; private set; } = false;

        public static bool DisableExitPrerequisite { get; private set; } = false;

        public static void changeDifficulty(Difficulty difficulty)
        {
            ChosenDifficulty = difficulty;
        }
        public static bool ToggleRevealMiniMap()
        {
            RevealMiniMap = !RevealMiniMap;
            return RevealMiniMap;
        }
        public static bool ToggleInvincibility()
        {
            Invincibility = !Invincibility;
            return Invincibility;
        }

        public static bool ToggleExitPrerequisite()
        {
            DisableExitPrerequisite = !DisableExitPrerequisite;
            return DisableExitPrerequisite;
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
