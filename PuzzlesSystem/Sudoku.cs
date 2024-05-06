

namespace PuzzleSystem
{

    public struct SudokuAction
    {
        public Point Position { init; get; }
        public int OldValue { init; get; }
        public int NewValue { init; get; }

        public SudokuAction(Point position, int oldValue, int newValue)
        {
            Position = position;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }

    public class Sudoku : Puzzle
    {
        private const string SUDOKU_DIRECTORY_PATH = "..\\..\\..\\PuzzlesSystem\\Puzzles\\Sudoku";

        private int _needToSolveCount = 0;

        private Stack<SudokuAction> _PriorActions = new Stack<SudokuAction>(45);
        private Stack<SudokuAction> _NextActions = new Stack<SudokuAction>(5);
        private SudokuAction _lastAction;

        public bool IsSolved
        {
            get
            {
                return _needToSolveCount == 0;
            }
        }

        public Sudoku()
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
                    SudokuTile element = GenerateSudokuElement(pair);
                    AddElement(element, i, j);
                    sudokuPairsIndex++;
                }
            }
        }

        private SudokuTile GenerateSudokuElement(string pair)
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

            return new SudokuTile(type, value);
        }

        public bool InputValueAtSolver(int newValue, bool isMoveNew)
        {
            SudokuTile elementSolverIsOn = (SudokuTile)_solver.WalkableElementOnTopOf;
            if (elementSolverIsOn.Type == SudokuElementType.Given)
            {
                return IsSolved;
            }

            int oldValue = elementSolverIsOn.CurrentValue;

            if (oldValue == newValue)
            {
                return IsSolved;
            }

            SudokuAction action = new SudokuAction(new Point(_solver.Position), oldValue, newValue);

            if (isMoveNew)
            {
                _PriorActions.Push(action);
                if (_NextActions.Count != 0)
                {
                    _NextActions.Clear();
                }
            }

            InputValue(elementSolverIsOn, newValue, _solver.Position);

            _solver.UpadteBackground();

            return IsSolved;
        }

        private void InputValue(SudokuTile element, int newValue, Point position)
        {
            _lastAction = new SudokuAction(position, element.CurrentValue, newValue);
            CheckForErroneousInput(element, element.CurrentValue, newValue, position);

            bool wasSolved = element.IsSolved;

            element.InputValue(newValue);

            if (element.IsSolved && !wasSolved)
            {
                _needToSolveCount--;
            }
            else if (!element.IsSolved && wasSolved)
            {
                _needToSolveCount++;
            }
        }

        public void UndoAction()
        {
            if (_PriorActions.Count != 0)
            {
                SudokuAction action = _PriorActions.Pop();
                InputValueAtPosition(action.OldValue, action.Position);
                _NextActions.Push(action);
                _solver.UpadteBackground();
            }
                
        }

        public void RedoAction()
        {
            if(_NextActions.Count != 0)
            {
                SudokuAction action = _NextActions.Pop();
                InputValueAtPosition(action.NewValue, action.Position);
                _PriorActions.Push(action);
                _solver.UpadteBackground();
            }   
        }

        private void InputValueAtPosition(int newValue, Point position)
        {
            Element element = ElementAt(position);

            if (element is PuzzleSolver)
            {
                InputValueAtSolver(newValue,false);
            }
            if (element is SudokuTile sudokuElement)
            {
                InputValue(sudokuElement, newValue, position);
            }
        }

        private void CheckForErroneousInput(SudokuTile element, int oldValue, int newValue, Point elementPosition)
        {
            bool boxResult = CheckForErroneousInBox(element, oldValue, newValue, elementPosition);
            bool rowResult = CheckForErroneousInRow(element, oldValue, newValue, elementPosition);
            bool columnResult = CheckForErroneousInColumn(element, oldValue, newValue, elementPosition);

            if(!boxResult && !rowResult && !columnResult)
            {
                element.TurnOffErronoues();
            }
        }

        private bool CheckForErroneousInColumn(SudokuTile element, int oldValue, int newValue, Point position)
        {
            bool wasErroneousFound = false;
            int column = position.X;
            for (int row = 1; row < 12; row++)
            {
                if (row == position.Y || row % 4 == 0)
                {
                    continue;
                }

                if (row == _lastAction.Position.Y)
                {
                    continue;
                }

                SudokuTile currentElement;

                if (ElementAt(row, column) is PuzzleSolver solver)
                {
                    currentElement = (SudokuTile)solver.WalkableElementOnTopOf;
                }
                else
                {
                    currentElement = (SudokuTile)ElementAt(row, column);
                }

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
        private bool CheckForErroneousInRow(SudokuTile element, int oldValue, int newValue, Point position)
        {
            bool wasErroneousFound = false;
            int row = position.Y;
            for (int column = 1; column < 12; column++)
            {
                if (column == position.X || column % 4 == 0)
                {
                    continue;
                }

                if (column == _lastAction.Position.X)
                {
                    continue;
                }

                SudokuTile currentElement;

                if (ElementAt(row, column) is PuzzleSolver solver)
                {
                    currentElement = (SudokuTile)solver.WalkableElementOnTopOf;
                }
                else
                {
                    currentElement = (SudokuTile)ElementAt(row, column);
                }

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

        private bool CheckForErroneousInBox(SudokuTile element, int oldValue, int newValue, Point position)
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

                    if (column == _lastAction.Position.X && row == _lastAction.Position.Y)
                    {
                        continue;
                    }

                    SudokuTile currentElement;

                    if (ElementAt(row, column) is PuzzleSolver solver)
                    {
                        currentElement = (SudokuTile)solver.WalkableElementOnTopOf;
                    }
                    else
                    {
                        currentElement = (SudokuTile)ElementAt(row, column);
                    }

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
