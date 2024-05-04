using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_System
{
    public abstract class ToggleButton : Button
    {

        private bool _toggle = false;

        public ToggleButton(string text) : base(text) {  }
        

        public override bool OnClick()
        {
            _toggle = true;
            return base.OnClick();
        }

        public void TurnOff()
        {
            _toggle = false;
        }

        public override string ToString()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            if (_toggle == false) Console.ForegroundColor = ConsoleColor.Red;

            return base.ToString();
        }
    }
}
