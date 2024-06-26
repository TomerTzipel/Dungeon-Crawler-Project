﻿

namespace SceneSystem
{
    public abstract class SelectionMenuScene : Scene
    {

        protected string _title;
        protected ConsoleColor _titleColor;

        protected Button[] _buttons;

        protected int _selectedButtonIndex;
        protected bool _wasButtonClicked;

        public SelectionMenuScene(int numberOfButtons, string title, ConsoleColor titleColor) : base(new SelectionMenuInputManager())
        {
            _title = title;
            _titleColor = titleColor;
            _buttons = new Button[numberOfButtons];
        }
        protected override void EnterScene()
        {
            _buttons[_selectedButtonIndex].Deselect();

            _wasButtonClicked = false;
            _selectedButtonIndex = 0;
            _buttons[0].Select();
            SceneManager.PrintCurrentScene();
        }
        public override void SceneLoop()
        {
            EnterScene();
            
            while (!_wasButtonClicked)
            {
                base.SceneLoop();
            }
        }

        protected override void HandleInput()
        {
            ConsoleKeyInfo input = _inputManager.LastInput;
            InputType inputType = _inputManager.LastInputType;

            switch (inputType)
            {
                case InputType.MenuMovement:
                    HandleMenuMovement(input);
                    break;
                case InputType.ButtonClick:
                    ActivateButton(_buttons[_selectedButtonIndex]);
                    break;
                default:
                    break;
            }
        }
        private void HandleMenuMovement(ConsoleKeyInfo input)
        {
            ConsoleKey inputKey = input.Key;

            switch (inputKey)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    PriorButton();
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    NextButton();
                    break;
            }

            SceneManager.PrintCurrentScene();
        }

        private void PriorButton()
        {
            _buttons[_selectedButtonIndex].Deselect();
            _selectedButtonIndex--;

            if (_selectedButtonIndex == -1)
            {
                _selectedButtonIndex = _buttons.Length - 1;
            }

            _buttons[_selectedButtonIndex].Select();
        }

        private void NextButton()
        {
            _buttons[_selectedButtonIndex].Deselect();
            _selectedButtonIndex++;

            if (_selectedButtonIndex == _buttons.Length)
            {
                _selectedButtonIndex = 0;
            }

            _buttons[_selectedButtonIndex].Select();
        }

        protected void ActivateButton(Button button)
        {
            bool clickResult = button.OnClick();

            if (clickResult)
            {
                _wasButtonClicked = true;
            }

        }

        public override void PrintScene()
        {
            Console.ForegroundColor = _titleColor;
            Console.WriteLine(_title);

            Printer.ColorReset();
            Console.WriteLine();

            for (int i = 0; i < _buttons.Length; i++)
            {
                Console.WriteLine(_buttons[i]);
                Printer.ColorReset();
                Console.WriteLine();
            }
        }
    }
}
