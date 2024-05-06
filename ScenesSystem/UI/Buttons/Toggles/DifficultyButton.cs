
namespace UI_System
{
    public class DifficultyButton : ToggleButton
    {
        private Difficulty _difficulty;

        public DifficultyButton(Difficulty difficulty) : base(difficulty.ToString())
        {
            _difficulty = difficulty;
        }

        public override bool OnClick()
        {
            Settings.changeDifficulty(_difficulty);
            return base.OnClick();
        }

    }
}
