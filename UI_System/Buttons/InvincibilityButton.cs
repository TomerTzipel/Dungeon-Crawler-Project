using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_System
{
    public class InvincibilityButton : ToggleButton
    {
        public InvincibilityButton() : base("Invincibility") { }

        public override bool OnClick()
        {
            _toggle = Settings.ToggleInvincibility();
            return true;
        }

    }
}
