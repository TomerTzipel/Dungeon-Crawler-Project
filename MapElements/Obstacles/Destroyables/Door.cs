
namespace MapElements
{
    public class Door : DestroyableElement
    {
        public Door() :base(DOOR_EI) { }

        protected bool _isOpened = false;

        public override bool HitBy(MovingElement element)
        {

            if (element is Player player)
            {
                if (!_isOpened)
                {
                    _isOpened = player.Inventory.UseKey();
                }
            }

            if (_isOpened)
            {
                return false;
            }

            return base.HitBy(element);
        }

        public override string ToString()
        {
            if (_isOpened) return EmptyElement.InnerInstance.ToString();

            return base.ToString();
        }
    }

    public class DoorBenDoor : Door { }

}
