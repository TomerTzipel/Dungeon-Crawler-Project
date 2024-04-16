

namespace Elements
{
    public enum SudokuElementType
    {
        Given, Missing
    }

    public class SudokuElement : WalkableElement
    {

        private int _solution;
        public int CurrentValue { get; private set; }

        public bool IsErroneous { get; private set; } = false;

        public SudokuElementType Type { get; private set; }

        public bool IsSolved {  get; private set; }

        public SudokuElement(SudokuElementType type, char value) :base(EMPTY_EI)
        {
            _solution = value - '0';
            Type = type;

            if (type == SudokuElementType.Given)
            {
                CurrentValue = _solution;
                IsSolved = true;
                Identifier = " " + CurrentValue;
                Foreground = GIVEN_NUMBER_EFC;
            }
            else
            {
                CurrentValue = 0;
                IsSolved = false;
                Foreground = MISSING_NUMBER_EFC;
            }
        }

        public void InputValue(int value)
        {
            CurrentValue = value;
            if(CurrentValue == 0)
            {
                Identifier = EMPTY_EI;
            }
            else
            {
                Identifier = " " + CurrentValue;
            }

            if (CurrentValue == _solution)
            {
                IsSolved = true;
            }
            else
            {
                IsSolved = false;
            }
        }

        public void TurnOnErronoues()
        {
            IsErroneous = true;
            Background = ERRONEOUS_EBC;
        }
        public void TurnOffErronoues()
        {
            IsErroneous = false;
            Background = DEFAULT_EBC;
        }
    }
}
