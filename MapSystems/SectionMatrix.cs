
namespace MapSystem
{
    public class SectionMatrix
    {
        protected readonly int _size;
        private readonly int _numberOfInnerSections;

        public Section[,] Sections { get; protected set; }

        public Point StartSectionPosition { get; protected set; }

        protected SectionMatrix(int size) 
        { 
            _size = size;
        }


        public SectionMatrix(int size,int numberOfInnerSectionsToGenerate)
        {
            _size = size;
            _numberOfInnerSections = numberOfInnerSectionsToGenerate;

            int numberOfInnerSectionsGenerated;
            do
            {
                Sections = new Section[_size, _size];
                GenerateStartSection();
                numberOfInnerSectionsGenerated = 1;
                GenerateAdjacentSections(StartSectionPosition, ref numberOfInnerSectionsGenerated);
            } while (numberOfInnerSectionsGenerated > numberOfInnerSectionsToGenerate);

            GenerateOuterSections();      
        }

        public Section SectionAt(Point position)
        {
            return Sections[position.Y,position.X];
        }
       
        
        public void GenerateSectionsLayout(MapComposition composition)
        {
            foreach (Section section in Sections)
            {
                if (section.Type == SectionType.Outer)
                {
                    continue;
                }

                if (section.Type != SectionType.Start && section.Type != SectionType.Exit)
                {
                    section.DecideType(composition, _numberOfInnerSections);
                }

                List<Direction> directionsOfEdges = FindEdges(section);
                section.GenerateLayout(directionsOfEdges);
            }

            //Case of Composition not fully used after first iteration
            bool wasLayoutGeneratedFromComposition = true;
            bool wasTakenFromComposition;
            while (!composition.IsEmpty() && wasLayoutGeneratedFromComposition)
            {
                wasLayoutGeneratedFromComposition = false;
                foreach (Section section in Sections)
                {
                    if (section.Type == SectionType.Inner)
                    {
                        wasTakenFromComposition = section.DecideType(composition, _numberOfInnerSections);
                        if(wasTakenFromComposition && !wasLayoutGeneratedFromComposition)
                        {
                            wasLayoutGeneratedFromComposition = true;
                        }

                        List<Direction> directionsOfEdges = FindEdges(section);
                        section.GenerateLayout(directionsOfEdges);
                    }

                    if (composition.IsEmpty()) break;
                }
            }
           
        }

        protected List<Direction> FindEdges(Section section)
        {
            List<Direction> edgesFound = new List<Direction>(4);

            Point directionToCheckPosition;
            List<Direction> directions = [Direction.Up, Direction.Down, Direction.Left, Direction.Right];
            bool edgeFound = false;

            foreach (Direction direction in directions)
            {
                directionToCheckPosition = new Point(section.SectionMatrixPosition);
                directionToCheckPosition.MovePointInDirection(direction);

                if (directionToCheckPosition.X < 0 || directionToCheckPosition.Y < 0)
                {
                    edgeFound = true;
                }

                if (directionToCheckPosition.X >= _size || directionToCheckPosition.Y >= _size)
                {
                    edgeFound = true;
                }

                if (!edgeFound && (SectionAt(directionToCheckPosition).Type == SectionType.Outer || SectionAt(directionToCheckPosition).Type == SectionType.ShipMid))
                {
                    edgeFound = true;
                }

                if (edgeFound)
                {
                    edgesFound.Add(direction);
                    edgeFound = false;
                }
            }

            return edgesFound;
        }

        protected void GenerateOuterSections()
        {
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    if (Sections[i,j] == null)
                    {
                        Sections[i,j] = new Section(j, i, SectionType.Outer);
                    }
                }
            }
        }

        private void GenerateStartSection()
        {
            int x = RandomIndex(_size);
            int y = RandomIndex(_size);
            StartSectionPosition = new Point(x, y);
            GenerateSectionAt(StartSectionPosition, SectionType.Start);
        }

        private void GenerateAdjacentSections(Point sectionPosition, ref int numberOfSectionsGenerated)
        {
            while (SectionAt(sectionPosition).Type != SectionType.Discontinue && numberOfSectionsGenerated < _numberOfInnerSections)
            {
                Direction chosenDirection = ChooseDirection(sectionPosition);

                if (chosenDirection == Direction.Error)
                {
                    SectionType sectionType = SectionAt(sectionPosition).Type;

                    if (sectionType != SectionType.Start && sectionType != SectionType.PsuedoStart)
                    {
                        SectionAt(sectionPosition).Mark(SectionType.Discontinue);
                        return;
                    }


                    Point psuedoStartSectionPosition = BreakFree(sectionPosition);

                    if (psuedoStartSectionPosition.X == -29)
                    {
                        numberOfSectionsGenerated = _numberOfInnerSections + 1;
                    }
                    else
                    {
                        GenerateAdjacentSections(psuedoStartSectionPosition, ref numberOfSectionsGenerated);
                    }

                    return;
                }

                Point newSectionPosition = new Point(sectionPosition.X, sectionPosition.Y);
                newSectionPosition.MovePointInDirection(chosenDirection);

                GenerateSectionAt(newSectionPosition);
                numberOfSectionsGenerated++;

                if (numberOfSectionsGenerated >= _numberOfInnerSections)
                {
                    SectionAt(newSectionPosition).Mark(SectionType.Exit);
                    return;
                }

                DecideIfEndSection(newSectionPosition);
                if (SectionAt(newSectionPosition).Type != SectionType.End)
                {
                    GenerateAdjacentSections(newSectionPosition, ref numberOfSectionsGenerated);
                }

                if (SectionAt(sectionPosition).Type != SectionType.Start && SectionAt(sectionPosition).Type != SectionType.PsuedoStart)
                {
                    DecideIfDiscontinuedSection(sectionPosition);
                }
            }
        }

        private Point BreakFree(Point sectionPosition)
        {
            List<Direction> directions = [Direction.Up, Direction.Down, Direction.Left, Direction.Right];
            int chosenDirectionIndex;
            Direction chosenDirection;

            while (directions.Count > 0)
            {
                chosenDirectionIndex = RandomIndex(directions.Count);
                chosenDirection = directions[chosenDirectionIndex];

                Point directedSectionPosition = new Point(sectionPosition);
                directedSectionPosition.MovePointInDirection(chosenDirection);

                if (directedSectionPosition.X >= _size || directedSectionPosition.Y >= _size)
                {
                    directions.RemoveAt(chosenDirectionIndex);
                    continue;
                }

                if (directedSectionPosition.X < 0 || directedSectionPosition.Y < 0)
                {
                    directions.RemoveAt(chosenDirectionIndex);
                    continue;
                }

                SectionType directedSectionType = SectionAt(directedSectionPosition).Type;
                if (directedSectionType != SectionType.Start && directedSectionType != SectionType.PsuedoStart)
                {
                    SectionAt(directedSectionPosition).Mark(SectionType.PsuedoStart);
                    return directedSectionPosition;
                }

                directions.RemoveAt(chosenDirectionIndex);
            }

            if (SectionAt(sectionPosition).Type == SectionType.Start)
            {
                return new Point(-29, 0);
            }

            return StartSectionPosition;
        }

        private void DecideIfEndSection(Point sectionPosition)
        {
            if (!DecideIfToContinueFromSection())
            {
                SectionAt(sectionPosition).Mark(SectionType.End);
            }
        }

        private void DecideIfDiscontinuedSection(Point sectionPosition)
        {
            if (!DecideIfToContinueFromSection())
            {
                SectionAt(sectionPosition).Mark(SectionType.Discontinue);
            }
        }

        private bool DecideIfToContinueFromSection()
        {
            return RollChance(CONTINUE_CHANCE);
        }

        private Direction ChooseDirection(Point sectionPosition)
        {
            List<Direction> directions = [Direction.Up, Direction.Down, Direction.Left, Direction.Right];
            int chosenDirectionIndex;
            Direction chosenDirection;
            bool isDirectionAvailable;

            while (directions.Count > 0)
            {
                chosenDirectionIndex = RandomIndex(directions.Count);
                chosenDirection = directions.ElementAt(chosenDirectionIndex);
                isDirectionAvailable = CheckDirection(sectionPosition, chosenDirection);
                if (isDirectionAvailable)
                {
                    return chosenDirection;
                }
                directions.RemoveAt(chosenDirectionIndex);
            }

            return Direction.Error;

        }

        private bool CheckDirection(Point sectionPosition, Direction direction)
        {
            Point directedSectionPosition = new Point(sectionPosition.X, sectionPosition.Y);
            directedSectionPosition.MovePointInDirection(direction);

            return IsSectionAvailable(directedSectionPosition);
        }

        private bool IsSectionAvailable(Point sectionPosition)
        {
            if (sectionPosition.X >= _size || sectionPosition.Y >= _size)
            {
                return false;
            }

            if (sectionPosition.X < 0 || sectionPosition.Y < 0)
            {
                return false;
            }

            if (SectionAt(sectionPosition) != null)
            {
                return false;
            }

            return true;
        }
        private void GenerateSectionAt(Point position)
        {
            Sections[position.Y, position.X] = new Section(position.X, position.Y);
        }

        private void GenerateSectionAt(Point position, SectionType type)
        {
            GenerateSectionAt(position);
            SectionAt(position).Mark(type);
        }

    }
}
