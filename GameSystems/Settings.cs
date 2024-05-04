

namespace GameSystems
{
    public static class Settings
    {
        public static Difficulty chosenDifficulty = Difficulty.Dynamic;

        public static void changeDifficulty(Difficulty difficulty)
        {
            chosenDifficulty = difficulty;
        }
    }
}
