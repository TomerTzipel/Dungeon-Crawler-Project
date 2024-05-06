

namespace GameSystem
{
    public class BossLevel : Level
    {
        public BossLevel(Player player) 
        {
            Map = new BossMap(player);
            GenerateMiniMap(Map.SectionsMatrix, 7);
        }
    }
}
