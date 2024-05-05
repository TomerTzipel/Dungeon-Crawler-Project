


namespace MapSystems
{
    public enum SectionType
    {
        //Game Sections  (Must be 0-3)
        Puzzle, Enemy, Chest, Trap, 

        //Generation Sections
        PsuedoStart, Discontinue, End, Inner, Outer, Start, Exit, 

        //Boss Fight Sections
        Boss, Spawner, BossStart, Gate, ShipLeft, ShipMid, ShipRight, EmptyInner,

        Error
    }

  

    public class Section
    {
        public Point MapPosition { get; private set; } //top left

        public  Point SectionMatrixPosition { get; private set; }

        public static readonly int Size = 5; //Change at your own risk, though it should work except the ship position at the boss stage

        public SectionType Type { get; private set; } = SectionType.Inner;

        public Element[,] SectionLayout { get; private set; }


        public Section(int x, int y)
        {
            Initialize(x, y);
        }

        public Section(int x, int y, SectionType type)
        {
            Initialize(x, y);
            Type = type;
        }

        private void Initialize(int x, int y)
        {
            SectionMatrixPosition = new Point(x, y);
            MapPosition = new Point(x * Size, y * Size);
            SectionLayout = new Element[Size,Size];

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    SectionLayout[i,j] = EmptyElement.OuterInstance;
                }
            }
        }

        public void Mark(SectionType sectionType)
        {
            Type = sectionType;
        }

        //True if taken from composition.
        public bool DecideType(MapComposition composition, int numberOfInnerSections)
        {
            Type = SectionType.Inner;

            if (composition.IsEmpty()) return false;

            float takeFromCompositionChance = 100 * (((float)composition.Total) / ((float)numberOfInnerSections));
            
            if (RollChance((int)takeFromCompositionChance))
            {
                bool wasPullSuccessful = false;
                while (!wasPullSuccessful)
                {
                    wasPullSuccessful = DecideTypeFromComposition(composition);
                }

                return true;
            }

            return false;   
        }

        private bool DecideTypeFromComposition(MapComposition composition)
        {
            int chosenType = RandomIndex(composition.Size);
            SectionType result = composition.PullType(chosenType);

            if(result == SectionType.Error)
            {
                return false;
            }

            Type = result;
            return true;
        }

        public void GenerateLayout(List<Direction> directionsOfEdges)
        {
            if (Type == SectionType.Outer) return;

            bool thereAreEdges = directionsOfEdges.Count != 0;  

            if(Type == SectionType.ShipLeft || Type == SectionType.ShipMid || Type == SectionType.ShipRight)
            {
                GenerateShipLayout();
                return;
            }

            ChangeOuterElementsToInner();

            if (thereAreEdges)
            {
                GenerateEdges(directionsOfEdges);
            }
   
            switch (Type)
            {
           
                case SectionType.Start:
                    GenerateStartLayout();
                    break;

                case SectionType.Exit:
                    GenerateExitLayout();
                    break;

                case SectionType.Trap:
                    GenerateTrapLayout();
                    break;

                case SectionType.Enemy:
                    GenerateEnemyLayoutByLevel();
                    break;

                case SectionType.Spawner:
                    GenerateSpawnerLayout();
                    break;

                case SectionType.Puzzle:
                    if (thereAreEdges)
                    {
                        GenerateEdgePuzzleLayout();
                    }
                    else
                    {
                        GenerateNormalPuzzleLayout();
                    }
                    break;

                case SectionType.Inner:
                    GenerateInnerLayout(true);
                    break;

                case SectionType.Chest:
                    GenerateChestLayout();
                    break;

                case SectionType.Boss:
                    GenerateBossLayout();
                    break;

                case SectionType.Gate:
                    GenerateGateLayout();
                    break;

                case SectionType.BossStart:
                    GenerateBossStartLayout();
                    break;
            }
        }

        private void ChangeOuterElementsToInner()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if(SectionLayout[i, j] == EmptyElement.OuterInstance)
                    {
                        SectionLayout[i, j] = EmptyElement.InnerInstance;
                    }
   
                }
            }
        }
        private void GenerateEdges(List<Direction> directionsOfEdges)
        {
            foreach (Direction direction in directionsOfEdges)
            {
                GenerateEdgeInDirection(direction);
            }
        }
        private void GenerateEdgeInDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:

                    for (int i = 0; i < Size; i++)
                    {
                        SectionLayout[0,i] = ObstacleElement.HorizontalWallInstance;
                    }
                    break;

                case Direction.Down:

                    for (int i = Size - 1; i >= 0; i--)
                    {
                        SectionLayout[Size - 1,i] = ObstacleElement.HorizontalWallInstance;
                    }
                    break;

                case Direction.Left:

                    for (int i = 0; i < Size; i++)
                    {
                        SectionLayout[i,0] = ObstacleElement.VerticalWallInstance;
                    }
                    break;


                case Direction.Right:

                    for (int i = Size - 1; i >= 0; i--)
                    {
                        SectionLayout[i,Size - 1] = ObstacleElement.VerticalWallInstance;
                    }
                    break;
            }
        }
        
        private void GenerateStartLayout()
        {
            SectionLayout[Size / 2, Size / 2] = new WalkableElement(ENTRY_EI);  
        }

        private void GenerateExitLayout()
        {
            SectionLayout[Size / 2, Size / 2] = new ExitElement();
        }

        private void GenerateNormalPuzzleLayout()
        {
            SectionLayout[Size / 2, Size / 2] = new PuzzleTeleportElement();
            List<Direction> directions = [Direction.Up, Direction.Down, Direction.Left, Direction.Right];
            GenerateEdges(directions);
            GenerateDoors();

        }
        private void GenerateEdgePuzzleLayout()
        {
            SectionLayout[Size / 2, Size / 2] = new PuzzleTeleportElement();
        }

        private void GenerateInnerLayout(bool spawnEnemies)
        {
            if(!RollChance(BIOME_CHANCE))
            {
                return;
            }

            BiomeType biomeType = RandomBiome();
            DecorationType[] decoration = GetDecorationByBiome(biomeType);
 
            float chance = 100f / (Size * Size);

            bool wasPlaced = false;
            bool isThereSpace = false;
            DecorationType currentDecorationType;

            for (int k = 0; k < decoration.Length; k++)
            {
                currentDecorationType = decoration[k];

                while (!wasPlaced)
                {
                    for (int i = 0; i < Size; i++)
                    {
                        for (int j = 0; j < Size; j++)
                        {
                            Element currentElement = SectionLayout[i, j];
                            if (currentElement is EmptyElement)
                            {
                                isThereSpace = true;
                                wasPlaced = AttemptToPlaceDecor(currentDecorationType, i, j, (int)chance);

                            }

                            if (wasPlaced) break;
                        }

                        if (wasPlaced) break;
                    }

                    if (!isThereSpace) break;      
                }

                if (!isThereSpace) break;

                isThereSpace = false;
                wasPlaced = false;
            }

            if (spawnEnemies)
            {
                EnemyType[] enemies = GetEnemiesByBiome(biomeType);
                GenerateEnemyLayout(enemies);
            }
        }

        private bool AttemptToPlaceDecor(DecorationType type, int row, int column, int chance)
        {
            if (RollChance(chance))
            {
                Element element = null;

                switch (type)
                {
                    case DecorationType.Boulder:
                        element = new BoulderElement();
                        break;

                    case DecorationType.Tree:
                        element = ObstacleElement.TreeInstance;
                        break;

                    case DecorationType.Grass:
                        element = new WalkableElement(GRASS_EI,GRASS_EFC);
                        break;

                    case DecorationType.Pebble:
                        element = new PebbleElement();
                        break;

                    case DecorationType.Vase:
                        element = new VaseElement();
                        break;

                    case DecorationType.Bush:
                        element = new BushElement();
                        break;

                    case DecorationType.VWall:
                        element = ObstacleElement.VerticalWallInstance;
                        break;

                    case DecorationType.HWall:
                        element = ObstacleElement.HorizontalWallInstance;
                        break;
                }

                SectionLayout[row, column] = element;

                return true;
            }

            return false;
        }

        private void GenerateSpawnerLayout()
        {
            Point spawnerPosition = CalculateMapPositionByLayoutPosition(Size / 2, Size / 2);

            SpawnerElement spawner = new SpawnerElement(spawnerPosition);
            SectionLayout[Size / 2, Size / 2] = spawner;
        }

        private void GenerateChestLayout()
        {
            SectionLayout[Size / 2, Size / 2] = new ChestElement();
        }

        private void GenerateBossStartLayout()
        {
            GenerateStartLayout();
            SectionLayout[0, 0] = ObstacleElement.VerticalWallInstance;
            SectionLayout[0, Size - 1] = ObstacleElement.VerticalWallInstance;
            SectionLayout[Size - 1, 0] = ObstacleElement.VerticalWallInstance;
            SectionLayout[Size - 1, Size - 1] = ObstacleElement.VerticalWallInstance;
        }

        private void GenerateGateLayout()
        {
            for (int i = 0; i < Size; i++)
            {
                SectionLayout[Size - 1, i] = GateElement.Instance;
            }
        }

        private void GenerateBossLayout()
        {
            Point[] position = [CalculateMapPositionByLayoutPosition(3, 2),
                                CalculateMapPositionByLayoutPosition(2, 2),
                                CalculateMapPositionByLayoutPosition(1, 2),
                                CalculateMapPositionByLayoutPosition(1, 1),
                                CalculateMapPositionByLayoutPosition(0, 1)];

            Basilisk basilisk = new Basilisk(position);

            basilisk.PlaceInSection(this);
        }

        private void GenerateShipLayout()
        {
            switch (Type)
            {
                case SectionType.ShipLeft:
                    SectionLayout[1, 3] = ShipSegmentElement.Instance;
                    SectionLayout[1, 4] = ShipSegmentElement.Instance;
                    SectionLayout[3, 3] = ShipSegmentElement.Instance;
                    SectionLayout[3, 4] = ShipSegmentElement.Instance;

                    for (int i = 0; i < Size; i++)
                    {
                        SectionLayout[2, i] = ShipSegmentElement.Instance;
                    }
                    break;

                case SectionType.ShipMid:
                    for (int i = 0; i < Size; i++)
                    {
                        for (int j = 0; j < Size; j++)
                        {
                            if (j == 0 && (i == 0 || i == Size - 1)) continue;

                            SectionLayout[i, j] = ShipSegmentElement.Instance;
                        }
                    }
                    break;

                case SectionType.ShipRight:
                    for (int i = 1; i <= 3; i++)
                    {
                        for (int j = 0; j <= 2; j++)
                        {
                            SectionLayout[i, j] = ShipSegmentElement.Instance;
                        }
                    }
                    SectionLayout[2, 3] = ShipSegmentElement.Instance;

                    break;
            }
        }

        private void GenerateEnemyLayoutByLevel()
        {
            EnemyType[] enemies = GetEnemiesByCurrentLevel();
            GenerateEnemyLayout(enemies);

        }
        private void GenerateEnemyLayout(EnemyType[] enemies)
        {
            if(enemies.Length == 0)
            {
                return;
            }

            float chance = 100f / (Size * Size);

            bool wasPlaced = false;
            bool isThereSpace = false;
            EnemyType currentEnemyType;

            for (int k = 0; k < enemies.Length; k++)
            {
                currentEnemyType = enemies[k];

                while (!wasPlaced)
                {
                    for (int i = 0; i < Size; i++)
                    {
                        for (int j = 0; j < Size; j++)
                        {
                            Element currentElement = SectionLayout[i, j];
                            if (currentElement is EmptyElement)
                            {
                                isThereSpace = true;
                                wasPlaced = AttemptToPlaceEnemy(currentEnemyType, i, j, (int)chance);
                            }

                            if (wasPlaced) break;
                        }

                        if (wasPlaced) break;
                    }
                    if (!isThereSpace) break;
                }

                if (!isThereSpace) break;

                isThereSpace = false;

                wasPlaced = false;
            }
        }

       

        private bool AttemptToPlaceEnemy(EnemyType type, int row, int column, int chance)
        {
            if (RollChance(chance))
            {
                Point spawnPosition = CalculateMapPositionByLayoutPosition(row, column);

                switch (type)
                {
                    case EnemyType.Bat:
                        SectionLayout[row, column] = new Bat(spawnPosition);
                        break;

                    case EnemyType.Slime:
                        SectionLayout[row, column] = new Slime(spawnPosition,3);
                        break;

                    case EnemyType.Ogre:
                        SectionLayout[row, column] = new Ogre(spawnPosition);
                        break;

                }
                
                return true;
            }

            return false;
        }
        private void GenerateTrapLayout()
        {
            GenerateInnerLayout(false);

            int chosenTrap = RandomRange(1,2);

            if (chosenTrap == 1)
            {
                SectionLayout[Size / 2, Size / 2] = new DamageTrapElement();
            }
            else
            {
                SectionLayout[Size / 2, Size / 2] = new QuicksandTrapElement();
            }
            
        }

        private void GenerateDoors()
        {
            List<Direction> directions = [Direction.Up, Direction.Down, Direction.Left, Direction.Right];
            DoorElement door = new DoorElement();
            foreach (Direction direction in directions)
            {
                switch (direction)
                {
                    case Direction.Up:

                        SectionLayout[0, Size / 2] = door;
                        break;

                    case Direction.Down:

                        SectionLayout[Size - 1, Size / 2] = door;
                        break;

                    case Direction.Left:

                        SectionLayout[Size / 2, Size - 1] = door;
                        break;

                    case Direction.Right:

                        SectionLayout[Size / 2, 0] = door;
                        break;
                }
            }
           
        }

        private Point CalculateMapPositionByLayoutPosition(int row, int col)
        {
            Point mapPosition = new Point(MapPosition);

            for (int i = 0; i < row; i++)
            {
                mapPosition.MovePointInDirection(Direction.Down);
            }

            for (int j = 0; j < col; j++)
            {
                mapPosition.MovePointInDirection(Direction.Right);
            }

            return mapPosition;
        }   


}
}
