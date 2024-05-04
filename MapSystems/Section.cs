

namespace MapSystems
{
    public enum SectionType
    {
        //Game Sections
        Puzzle, Enemy, Chest, Trap, Spawner,

        //Generation Sections
        PsuedoStart, Discontinue, End, Inner, Outer, Start, Exit, Error
    }

  

    public class Section
    {
        public Point MapPosition { get; private set; } //top left

        public  Point SectionMatrixPosition { get; private set; }

        public static readonly int Size = 5; // Move to a Consts file

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

            bool thereAreEdges = directionsOfEdges.Count != 0;  

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
                    GenerateInnerLayout();
                    break;

                case SectionType.Chest:
                    GenerateChestLayout();
                    break;
            }
        }

        private void ChangeOuterElementsToInner()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    SectionLayout[i, j] = EmptyElement.InnerInstance;
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
            SectionLayout[Size / 2, Size / 2] = new WalkableElement(ENTRY_EI);  //PlayerManager.Instance.PlayerElement.WalkOn();
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

        private void GenerateInnerLayout()
        {
            if(!RollChance(BIOME_CHANCE))
            {
                return;
            }

            BiomeType biomeType = RandomBiome();
            DecorationType[] decoration = GetDecorationByBiome(biomeType);
            EnemyType[] enemies = GetEnemiesByBiome(biomeType);

            float chance = 100f / (Size * Size);

            bool wasPlaced = false;
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
                            if (currentElement is EmptyElement && !wasPlaced)
                            {
                                wasPlaced = AttemptToPlaceDecor(currentDecorationType, i, j, (int)chance);

                            }
                        }
                    }
                }

                wasPlaced = false;
            }

            GenerateEnemyLayout(enemies);

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
        private void GenerateEnemyLayoutByLevel()
        {
            EnemyType[] enemies = EnemiesLibrary.GetEnemiesByCurrentLevel();
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
                            if (currentElement is EmptyElement && !wasPlaced)
                            {
                                wasPlaced = AttemptToPlaceEnemy(currentEnemyType, i, j, (int)chance);

                            }
                        }
                    }
                }

                wasPlaced = false;
            }
        }

       

        private bool AttemptToPlaceEnemy(EnemyType type, int row, int column, int chance)
        {
            if (RollChance(chance))
            {
                Point spawnPosition = CalculateMapPositionByLayoutPosition(row, column);

                EnemyElement enemy = null;

                switch (type)
                {
                    case EnemyType.Bat:
                        enemy = new Bat(spawnPosition);
                        break;

                    case EnemyType.Slime:
                        enemy = new Slime(spawnPosition,3);
                        break;

                    case EnemyType.Ogre:
                        enemy = new Ogre(spawnPosition);
                        break;

                }

                SectionLayout[row, column] = enemy;

                return true;
            }

            return false;
        }
        private void GenerateTrapLayout()
        {
            GenerateInnerLayout();

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
