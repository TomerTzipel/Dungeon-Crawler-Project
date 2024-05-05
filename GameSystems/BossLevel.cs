using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystems
{
    public class BossLevel : Level
    {
        public BossLevel(PlayerElement player) 
        {
            Map = new BossMap(player);
            GenerateMiniMap(Map.SectionsMatrix, 7);
        }
    }
}
