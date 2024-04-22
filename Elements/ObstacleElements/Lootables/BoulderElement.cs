
namespace Elements
{
    public class BoulderElement : DestroyableElement
    {
        public BoulderElement() :base(BOULDER_EI) 
        {
            Foreground = BOULDER_EFC;
            _hp = RandomRange(10, 20);
        }

        protected override void Destroyed()
        {
            LootManager.GenerateLoot(LootOrigin.Boulder); 
        }
    }
}
