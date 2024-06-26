﻿

namespace MapSystem
{
    public class MiniMapSection
    {
        public SectionType Type { get; private set; }

        public bool WasDiscovered { get; private set; } = false;
        public bool WasSurroundingDiscovered { get; set; } = false;
        public MiniMapSection(Section section) 
        {
            Type = section.Type;

            if (Settings.RevealMiniMap) Discover();

        }
        private ConsoleColor SectionColor
        {
            get
            {
                if (!WasDiscovered)
                {
                    return ConsoleColor.Black;
                }

                switch (Type)
                {
                    case SectionType.Puzzle:
                        return ConsoleColor.Magenta;

                    case SectionType.Chest:
                        return ConsoleColor.DarkYellow;

                    case SectionType.Spawner:
                        return ConsoleColor.Red;

                    case SectionType.Outer:
                        return ConsoleColor.Blue;

                    case SectionType.Start:
                        return ConsoleColor.Cyan;

                    case SectionType.Exit:
                        return ConsoleColor.Green;

                    case SectionType.ShipLeft:
                    case SectionType.ShipMid:
                    case SectionType.ShipRight:
                        return ConsoleColor.DarkYellow;
                }

                return ConsoleColor.White;
            }

        }
        public void Discover()
        {
            WasDiscovered = true;
        }

        public void Print()
        {
            Console.BackgroundColor = SectionColor;
            Console.Write("  ");
        }

       

    }
}
