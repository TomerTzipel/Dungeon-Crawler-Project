using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapSystems
{
    public class BossMap : Map
    {
        public BossMap(PlayerElement player) 
        {
            SectionsMatrix = new BossSectionMatrix();
            Size = 7 * Section.Size;
            Elements = new Element[Size, Size];

            GenerateMapFromSections();
            LocalizePlayer(player);
        }
    }
}
