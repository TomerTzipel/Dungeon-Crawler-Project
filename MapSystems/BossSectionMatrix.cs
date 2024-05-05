using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapSystems
{
    public class BossSectionMatrix : SectionMatrix
    {
        public BossSectionMatrix() : base(7)
        {
            Sections = new Section[_size, _size];
            GenerateBossSectionMatrix();
        }

        private void GenerateBossSectionMatrix()
        {
            GenerateOuterSections();
            MarkSectiopnsForBossSectionMatrix();
            StartSectionPosition = new Point(3, 4);
            GenerateSectionsLayout();

        }

        private void MarkSectiopnsForBossSectionMatrix()
        {
            Sections[0, 2].Mark(SectionType.ShipLeft);
            Sections[0, 3].Mark(SectionType.ShipMid);
            Sections[0, 4].Mark(SectionType.ShipRight);

            Sections[1, 3].Mark(SectionType.Exit);

            Sections[2, 3].Mark(SectionType.Gate);

            Sections[3, 2].Mark(SectionType.Spawner);
            Sections[3, 3].Mark(SectionType.Boss);
            Sections[3, 4].Mark(SectionType.Spawner);

            Sections[4, 2].Mark(SectionType.EmptyInner);
            Sections[4, 3].Mark(SectionType.BossStart);
            Sections[4, 4].Mark(SectionType.EmptyInner);

            Sections[5, 2].Mark(SectionType.Spawner);
            Sections[5, 3].Mark(SectionType.EmptyInner);
            Sections[5, 4].Mark(SectionType.Spawner);
        }

        protected void GenerateSectionsLayout()
        {
            foreach (Section section in Sections)
            {
                if (section.Type == SectionType.Outer)
                {
                    continue;
                }

                List<Direction> directionsOfEdges = FindEdges(section);
                section.GenerateLayout(directionsOfEdges);
            }
        }
    }
}
