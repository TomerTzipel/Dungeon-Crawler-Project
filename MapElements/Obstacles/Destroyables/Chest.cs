
namespace MapElements
{
    public class Chest : DestroyableElement
    {
        public Chest() : base(CHEST_EI) 
        {
            _hp = -1;
            Foreground = CHEST_EFC;
        }
        public override bool HitBy(MovingElement element)
        {

            if (element is Player player)
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
            Printer.AddActionText(ActionTextType.Item, "The chest contained:");
            LootManager.RewardEquipment();
        }
    }
}
