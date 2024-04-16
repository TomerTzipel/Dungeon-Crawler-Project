




using GameSystems;
using System.Diagnostics;
using System.Numerics;

namespace MapSystems
{
    public class Map
    {
        private SectionMatrix _sectionsMatrix;

        private static readonly object _dataLock = new object();

        private volatile Element[,] _elements;
        public int Size { init;  get; }

        protected Map() { }

        public Map(int numberOfSectionsToGenerate, int matrixSectionSize, MapComposition mapComposition, PlayerElement player)
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

        public void GenerateMapFromSections()
        {
            foreach (Section section in _sectionsMatrix.Sections) 
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
            _sectionsMatrix = new SectionMatrix(size, numberOfSectionsToGenerate);
            _sectionsMatrix.GenerateSectionsLayout(mapComposition);
        }

        public void LocalizePlayer(PlayerElement player)
        {
            int sectionSize = Section.Size;
            Point startPosition = new Point(_sectionsMatrix.StartSectionPosition.X * sectionSize, _sectionsMatrix.StartSectionPosition.Y * sectionSize);

            for (int i = 0; i < (sectionSize / 2); i++)
            {
                startPosition.MovePointInDirection(Direction.Right).MovePointInDirection(Direction.Down);
            }

            player.InitializePositionOnNewLevel(startPosition);
            player.WalkOn((WalkableElement)ElementAt(startPosition));
            AddElement(player, startPosition);
        }

        public void PrintMiniMap()
        {
            Console.WriteLine();
            _sectionsMatrix.Print();
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
                if(ElementAt(element.Position) != element){
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

                //Obstacle Case - which is do nothing for now
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
            if (element is PlayerElement player) 
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
