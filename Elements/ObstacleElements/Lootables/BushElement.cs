namespace Elements
{
    public class BushElement : DestroyableElement
    {
        public BushElement() : base(BUSH_EI)
        {
            Foreground = BUSH_EFC;
            _hp = RandomRange(5, 15);
        }

        protected override void Destroyed()
        {
            LootManager.GenerateLoot(LootOrigin.Bush);
        }
    }
}

