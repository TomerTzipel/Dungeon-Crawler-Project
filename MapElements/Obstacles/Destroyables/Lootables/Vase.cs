
namespace MapElements
{
    public class Vase : DestroyableElement
    {
        public Vase() :base(VASE_EI) 
        {
            Foreground = VASE_EFC;
            _hp = RandomRange(10, 20);
        }

        protected override void Destroyed()
        {
            LootManager.GenerateLoot(LootOrigin.Vase);
        }
    }
}
