

namespace UI_System
{
    public class PlayerColorButton : ToggleButton
    {
        private ConsoleColor _color;

        public PlayerColorButton(ConsoleColor color) : base(color.ToString())
        {
            _color = color;
        }

        public override bool OnClick()
        {
            Settings.ChangePlayerColor(_color);
            
            return base.OnClick();
        }
    }
}
