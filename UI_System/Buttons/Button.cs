using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_System
{
    public abstract class Button
    {
        private string _text;

        private bool _isSelected = false;

        public Button(string text) 
        {
            _text = text;
        }

        public void Select()
        {
            _isSelected = true;
        }
        public void Deselect()
        {
            _isSelected = false;
        }

        public virtual bool OnClick()
        {
            Deselect();
            return true;
        }
       
        public override string ToString()
        {
            if (_isSelected)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }

            return _text;
        }


    }
}
