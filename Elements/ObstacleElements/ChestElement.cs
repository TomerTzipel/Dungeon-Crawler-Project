
namespace Elements
{
    public class ChestElement : DestroyableElement
    {
        public ChestElement() : base(CHEST_EI) 
        {
            _hp = -1;
            Foreground = CHEST_EFC;
        }
        public override bool HitBy(MovingElement element)
        {

            if (element is PlayerElement player)
            {
                if (player.Inventory.UseKey())
                {
                    Destroyed();
                    return false;
                }
                
            }

            return base.HitBy(element);
        }

        protected override void Destroyed()
        {
            //reward drop  
        }
    }
}
