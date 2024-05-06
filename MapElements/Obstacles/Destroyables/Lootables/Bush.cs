namespace MapElements
{
    public class Bush : DestroyableElement
    {
        public Bush() : base(BUSH_EI)
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

