
namespace MapSystem
{
    public class BossMap : Map
    {
        public BossMap(Player player) 
        {
            SectionsMatrix = new BossSectionMatrix();
            Size = 7 * Section.Size;
            Elements = new Element[Size, Size];

            GenerateMapFromSections();
            LocalizePlayer(player);
        }
    }
}
