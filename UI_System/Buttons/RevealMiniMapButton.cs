using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_System
{
    public class RevealMiniMapButton : ToggleButton
    {
        public RevealMiniMapButton() : base("Mini Map Starts Revealed") { }

        public override bool OnClick()
        {
            _toggle = Settings.ToggleRevealMiniMap();
            return true;
        }

    }
}
