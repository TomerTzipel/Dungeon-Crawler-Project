using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_System
{
    public static class MainMenu
    {
        public static Button[] Buttons { get; private set; } = new Button[2];

        private static Button _selectedButton;
        private static int _selectedButtonIndex;
        public static void SetUpMainMenu()
        {
            Buttons[0] = new StartGameButton();
            //Buttons[1] = new CloseGameButton();

            _selectedButton = Buttons[0];
            _selectedButtonIndex = 0;
            Buttons[0].Select();
        }

        public static void PriorButton()
        {
            Buttons[_selectedButtonIndex].Deselect();
            _selectedButtonIndex--;

            if (_selectedButtonIndex == -1)
            {
                _selectedButtonIndex = Buttons.Length - 1;
            }

            Buttons[_selectedButtonIndex].Select();
        }

        public static void NextButton()
        {
            Buttons[_selectedButtonIndex].Deselect();
            _selectedButtonIndex++;

            if (_selectedButtonIndex == Buttons.Length)
            {
                _selectedButtonIndex = 0;
            }

            Buttons[_selectedButtonIndex].Select();
        }

        public static void ActivateSelectedButton()
        {
            _selectedButton.OnClick();
        }

        public static void Print()
        {
            for (int i = 0; i < Buttons.Length; i++)
            {
                Console.WriteLine(Buttons[i]);
                Printer.ColorReset();
                Console.WriteLine();
            }
        }

    }
}
