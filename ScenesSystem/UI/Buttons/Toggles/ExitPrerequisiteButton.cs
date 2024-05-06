

namespace UI_System
{
    internal class ExitPrerequisiteButton : ToggleButton
    {
        public ExitPrerequisiteButton() : base("Disable Exit Prerequisite") { }

        public override bool OnClick()
        {
            _toggle = Settings.ToggleExitPrerequisite();
            return true;
        }
    }
}
