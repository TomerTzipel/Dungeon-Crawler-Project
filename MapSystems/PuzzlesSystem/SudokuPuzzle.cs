
namespace MapSystems
{
    public class SudokuPuzzle : Puzzle
    {
        private const string SUDOKU_DIRECTORY_PATH = "..\\..\\..\\Puzzles\\Sudoku";

        private int _needToSolveCount = 0;

        public bool IsSolved
        {
            get
            {
                return _needToSolveCount == 0;
            }
        }

        public SudokuPuzzle()
        {
            Size = 13;
            Elements = new Element[Size, Size];
            string[] sudokuPuzzles = Directory.GetFiles(SUDOKU_DIRECTORY_PATH);
            int chosenPuzzle = RandomIndex(sudokuPuzzles.Length);

            GenerateSudokuFromFile(sudokuPuzzles[chosenPuzzle]);

            _solver.WalkOn((WalkableElement)ElementAt(1, 1));
            AddElement(_solver, 1, 1);
        }

        private void GenerateSudokuFromFile(string fileName)
        {
            string rawData = File.ReadAllText(fileName);
            string[] sudokuPairs = rawData.Replace("\r\n", "").Split(' ');

            int sudokuPairsIndex = 0;
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (j == 0 || j == Size - 1)
                    {
                        AddElement(ObstacleElement.VerticalWallInstance, i, j);
                        continue;
                    }
                    if (i == 0 || i == Size - 1)
                    {
                        AddElement(ObstacleElement.HorizontalWallInstance, i, j);
                        continue;
                    }

                    if (j % 4 == 0)
                    {
                        AddElement(SudokuPassWall.VerticalWallInstance, i, j);
                        continue;
                    }

                    if (i % 4 == 0)
                    {
                        AddElement(SudokuPassWall.HorizontalWallInstance, i, j);
                        continue;
                    }

                    string pair = sudokuPairs[sudokuPairsIndex];
                    SudokuElement element = GenerateSudokuElement(pair);
                    AddElement(element, i, j);
                    sudokuPairsIndex++;
                }
            }
        }

        private SudokuElement GenerateSudokuElement(string pair)
        {
            SudokuElementType type;
            if (pair[0] == 'G')
            {
                type = SudokuElementType.Given;
            }
            else
            {
                _needToSolveCount++;
                type = SudokuElementType.Missing;
            }

            char value = pair[1];

            return new SudokuElement(type, value);
        }

        public bool InputValue(int newValue)
        {
            if (_solver.WalkableElementOnTopOf == null)
            {
                return IsSolved;
            }

            SudokuElement elementSolverIsOn = (SudokuElement)_solver.WalkableElementOnTopOf;
            if (elementSolverIsOn.Type == SudokuElementType.Given)
            {
                return IsSolved;
            }

            int oldValue = elementSolverIsOn.CurrentValue;

            if (oldValue == newValue)
            {
                return IsSolved;
            }

            CheckForErroneousInput(elementSolverIsOn, oldValue, newValue, _solver.Position);

            bool wasSolved = elementSolverIsOn.IsSolved;

            _solver.InputValueToSudokuElement(newValue);



            if (elementSolverIsOn.IsSolved && !wasSolved)
            {
                _needToSolveCount--;
            }
            else if (!elementSolverIsOn.IsSolved && wasSolved)
            {
                _needToSolveCount++;
            }

            return IsSolved;
        }

        private void CheckForErroneousInput(SudokuElement element, int oldValue, int newValue, Point elementPosition)
        {
            bool boxResult = CheckForErroneousInBox(element, oldValue, newValue, elementPosition);
            bool rowResult = CheckForErroneousInRow(element, oldValue, newValue, elementPosition);
            bool columnResult = CheckForErroneousInColumn(element, oldValue, newValue, elementPosition);

            if(!boxResult && !rowResult && !columnResult)
            {
                element.TurnOffErronoues();
            }
        }

        private bool CheckForErroneousInColumn(SudokuElement element, int oldValue, int newValue, Point position)
        {
            bool wasErroneousFound = false;
            int column = position.X;
            for (int row = 1; row < 12; row++)
            {
                if (row == position.Y || row % 4 == 0)
                {
                    continue;
                }

                if (row == _solver.Position.Y)
                {
                    continue;
                }

                SudokuElement currentElement = (SudokuElement)ElementAt(row, column);

                if (newValue != 0 && currentElement.CurrentValue == newValue)
                {
                    wasErroneousFound = true;
                    currentElement.TurnOnErronoues();
                    element.TurnOnErronoues();
                }

                if (oldValue != 0 && currentElement.CurrentValue == oldValue && oldValue != newValue)
                {
                    CheckForErroneousInput(currentElement, currentElement.CurrentValue, currentElement.CurrentValue, new Point(column, row));
                }
            }

            return wasErroneousFound;

            
        }
        private bool CheckForErroneousInRow(SudokuElement element, int oldValue, int newValue, Point position)
        {
            bool wasErroneousFound = false;
            int row = position.Y;
            for (int column = 1; column < 12; column++)
            {
                if (column == position.X || column % 4 == 0)
                {
                    continue;
                }

                if (column == _solver.Position.X)
                {
                    continue;
                }

                SudokuElement currentElement = (SudokuElement)ElementAt(row, column);

                if (newValue != 0 && currentElement.CurrentValue == newValue)
                {
                    wasErroneousFound = true;
                    currentElement.TurnOnErronoues();
                    element.TurnOnErronoues();
                }

                if (oldValue != 0 && currentElement.CurrentValue == oldValue && oldValue != newValue)
                {
                    CheckForErroneousInput(currentElement, currentElement.CurrentValue, currentElement.CurrentValue, new Point(column, row));
                }
            }

            return wasErroneousFound;
        }

        private bool CheckForErroneousInBox(SudokuElement element, int oldValue, int newValue, Point position)
        {
            Point boxStart = FindBoxStart(position);

            bool wasErroneousFound = false;

            for (int row = boxStart.Y; row < boxStart.Y + 3; row++)
            {
                for (int column = boxStart.X; column < boxStart.X + 3; column++)
                {
                    if (column == position.X && row == position.Y)
                    {
                        continue;
                    }

                    if (column == _solver.Position.X && row == _solver.Position.Y)
                    {
                        continue;
                    }

                    SudokuElement currentElement = (SudokuElement)ElementAt(row, column);

                    if (newValue != 0 && currentElement.CurrentValue == newValue)
                    {
                        wasErroneousFound = true;
                        currentElement.TurnOnErronoues();
                        element.TurnOnErronoues();
                    }

                    if (oldValue != 0 && currentElement.CurrentValue == oldValue && oldValue != newValue)
                    {
                        CheckForErroneousInput(currentElement, currentElement.CurrentValue, currentElement.CurrentValue, new Point(column, row));
                    }
                }
            }

            return wasErroneousFound;
        }

        private Point FindBoxStart(Point position)
        {
            int row, columb;

            if(position.Y < 4)
            {
                row = 1;
            }
            else if(position.Y < 8)
            {
                row = 5;
            }
            else 
            { 
                row = 9;
            }

            if (position.X < 4)
            {
                columb = 1;
            }
            else if (position.X < 8)
            {
                columb = 5;
            }
            else
            {
                columb = 9;
            }

            return new Point(columb, row);
        }
    }
}
