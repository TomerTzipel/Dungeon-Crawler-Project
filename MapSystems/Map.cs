
using System.Diagnostics;


namespace MapSystem
{
    public class Map
    {
        public SectionMatrix SectionsMatrix { get; protected set; }

        private static readonly object _dataLock = new object();

        private volatile Element[,] _elements;
        public int Size { init;  get; }

        protected Map() { }

        public Map(int numberOfSectionsToGenerate, int matrixSectionSize, MapComposition mapComposition, Player player)
        {
            GenerateSections(numberOfSectionsToGenerate, matrixSectionSize, mapComposition);
            Size = matrixSectionSize * Section.Size;
            Elements = new Element[Size,Size];

            GenerateMapFromSections();
            LocalizePlayer(player);
           
        }

        public Element[,] Elements
        {
            get{ return _elements; }

            protected set { _elements = value; }
        }

        public void AddElement(Element element, Point position)
        {
            AddElement(element, position.Y, position.X);
        }
        public void AddElement(Element element, int row, int column)
        {
            lock (_dataLock)
            {
                Elements[row, column] = element;
            }
        }

        public Element ElementAt(Point position)
        {
            return ElementAt(position.Y,position.X);
        }

        public Element ElementAt(int row, int column)
        {
            lock (_dataLock)
            {
                return Elements[row, column];
            }
        }

        protected void GenerateMapFromSections()
        {
            foreach (Section section in SectionsMatrix.Sections) 
            {
                ExtractSectionToMap(section);
            }
        }

        protected void ExtractSectionToMap(Section section)
        {
            Element[,] sectionLayout = section.SectionLayout;
            
            for (int i = 0; i < Section.Size; i++) 
            { 
                for (int j = 0; j < Section.Size; j++) 
                {
                    Point positionOnMap = section.MapPosition;
                    AddElement(sectionLayout[i, j], positionOnMap.Y + i, positionOnMap.X + j);
                }
            }
        }

        protected void GenerateSections(int numberOfSectionsToGenerate, int size, MapComposition mapComposition)
        {
            SectionsMatrix = new SectionMatrix(size, numberOfSectionsToGenerate);
            SectionsMatrix.GenerateSectionsLayout(mapComposition);
        }

        protected void LocalizePlayer(Player player)
        {
            int sectionSize = Section.Size;
            Point startPosition = new Point(SectionsMatrix.StartSectionPosition.X * sectionSize, SectionsMatrix.StartSectionPosition.Y * sectionSize);

            for (int i = 0; i < (sectionSize / 2); i++)
            {
                startPosition.MovePointInDirection(Direction.Right).MovePointInDirection(Direction.Down);
            }

            player.InitializePositionOnNewLevel(startPosition);
            player.WalkOn((WalkableElement)ElementAt(startPosition));
            AddElement(player, startPosition);
        }

        public void PrintToCamera(Camera camera)
        {
            lock (_dataLock)
            {
                Point pivot = camera.Pivot;
                int width = pivot.X + camera.Width;
                int height = pivot.Y + camera.Height;

                for (int i = pivot.Y; i < height; i++)
                {
                    for (int j = pivot.X; j < width; j++)
                    {
                        Element elementToRender;
                        if (i < 0 || i >= Size || j < 0 || j >= Size)
                        {
                            elementToRender = EmptyElement.OuterInstance;
                        }
                        else
                        {
                            elementToRender = ElementAt(i, j);
                        }

                        Printer.CheckColors(elementToRender);
                        Console.Write(elementToRender);

                    }
                    Console.WriteLine();
                }
            }
        }

        public bool MoveElementInDirection(MovingElement element, Direction direction)
        {
            lock (_dataLock)
            {
                //This if statement isn't nesseccery as the bug that caused me to write it was fixed.
                //Though it is better to keep it in case a new bug creates the same bug of ghost enemies.
                //Future Me: Can confirm it was smart to keep
                if (ElementAt(element.Position) != element){
                    Debug.WriteLine("Ghost");
                    return false;
                }

                CheckMovingElementStateShift(element, direction);

                if (element.IsStunned)
                {
                    element.ProgressStunStatus(direction);
                    return false;
                }

                Point newPosition = new Point(element.Position);

                newPosition.MovePointInDirection(direction);

                if (ElementAt(newPosition) is MovingElement movingElement)
                {
                    bool isMovementAllowed = element.CollideWith(movingElement,this);

                    if (!isMovementAllowed)
                    {
                        return false;
                    }
                }

                if (ElementAt(newPosition) is EmptyElement)
                {
                    MoveElement(element, newPosition, direction);
                    return true;
                }

                if (ElementAt(newPosition) is DestroyableElement destroyableElement)
                {
                    element.CollideWith(destroyableElement, this);

                    bool collisionResult = destroyableElement.HitBy(element);
                    if (!collisionResult)
                    {
                        MoveElement(element, newPosition, direction);
                        return true;
                    }
                }

              

                if (ElementAt(newPosition) is WalkableElement walkableElement)
                {
                    MoveElement(element, newPosition, direction);
                    element.WalkOn(walkableElement);

                    if (walkableElement is SudokuPassWall)
                    {
                        MoveElementInDirection(element, direction);
                    }

                    return true;
                }

                if (ElementAt(newPosition) is ObstacleElement obstacleElement && !(ElementAt(newPosition) is DestroyableElement))
                {
                    element.CollideWith(obstacleElement, this);
                }

                return false;
            }
        }

        private void MoveElement (MovingElement element, Point newPosition, Direction direction)
        {
            Point oldPosition = new Point(element.Position.X, element.Position.Y);

            element.Move(direction);

            if (element.IsOnWalkableElement)
            {
                WalkableElement walkableElement = element.WalkableElementOnTopOf;
                element.WalkOff();
                AddElement(ElementAt(oldPosition), newPosition);
                AddElement(walkableElement, oldPosition);
                return;
            }


            AddElement(ElementAt(oldPosition), newPosition);
            AddElement(EmptyElement.InnerInstance, oldPosition);
           
        }

        private void CheckMovingElementStateShift(MovingElement element, Direction direction)
        {
            if (element is Player player) 
            {
                player.ChangeDirection(direction);
                return;
            }

            if(element is Bat bat)
            {
                bat.ChangeState();
            }

        }
        

    }
}
