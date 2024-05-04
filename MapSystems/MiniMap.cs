

namespace MapSystems
{
    public class MiniMap
    {
        private readonly int _size;

        public bool DoesRequireReprint { get; set; } = false;

        public MiniMapSection[,] Sections { get; private set; }


        public MiniMap(SectionMatrix sectionMatrix, int size) 
        {
            _size = size;
            Sections = new MiniMapSection[_size, _size];

            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    Sections[i, j] = new MiniMapSection(sectionMatrix.Sections[i,j]);
                }
            }

            DiscoverSectionsAroundPlayer();
        }

        public void DiscoverSectionsAroundPlayer()
        {
            Point playerPosition = PlayerManager.PlayerElement.Position;

            Point sectionPlayerAtPosition = new Point(playerPosition.X / Section.Size, playerPosition.Y / Section.Size);

            MiniMapSection sectionPlayerAt = Sections[sectionPlayerAtPosition.Y, sectionPlayerAtPosition.X];


            if (sectionPlayerAt.WasSurroundingDiscovered) return;

            

            sectionPlayerAtPosition.MovePointInDirection(Direction.Up).MovePointInDirection(Direction.Left);

            for(int i = sectionPlayerAtPosition.Y; i < sectionPlayerAtPosition.Y + 3; i++)
            {
                for (int j = sectionPlayerAtPosition.X; j < sectionPlayerAtPosition.X + 3; j++)
                {
                    if (i < 0 || j < 0 || i >= _size || j >= _size)
                    {
                        continue;
                    }
                    MiniMapSection currentSection = Sections[i, j];

                    if (!currentSection.WasDiscovered)
                    {
                        currentSection.Discover();
                        DoesRequireReprint = true;
                    }     
                }
            }

            sectionPlayerAt.WasSurroundingDiscovered = true;
        }

        public void Print()
        {
            Console.SetCursorPosition((Printer.CAMERA_WIDTH * 2) + 1, 0);
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    Sections[i, j].Print();
                    //Reset color unless doing a color check
                    Printer.ColorReset();
                }
                Console.SetCursorPosition((Printer.CAMERA_WIDTH * 2) + 1, i + 1);
            }
        }
    }
}
