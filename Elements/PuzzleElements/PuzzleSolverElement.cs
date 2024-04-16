﻿

using System.Xml.Linq;

namespace Elements
{
    public class PuzzleSolverElement : MovingElement
    {
        public PuzzleSolverElement() : base(new Point(1,1),PUZZLE_SOLVER_EI)
        {
            Foreground = PUZZLE_SOLVER_EFC;
        }

        public void InputValueToSudokuElement(int value)
        {
            SudokuElement sudokuElement = (SudokuElement)WalkableElementOnTopOf;
            sudokuElement.InputValue(value);

            _originalBackgroundColor = Background;
            Background = WalkableElementOnTopOf.Background;
        }

        public override string ToString()
        {
            if (IsOnWalkableElement)
            {
                return Identifier + WalkableElementOnTopOf.Identifier[1];
            }

            return Identifier + EmptyElement.InnerInstance.Identifier[1]; 
            
        }


    }
}