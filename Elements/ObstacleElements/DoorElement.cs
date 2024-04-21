
namespace Elements
{
    public class DoorElement : DestroyableElement
    {
        public DoorElement() :base(DOOR_EI) { }

        private bool _isOpened = false;

        public override bool HitBy(MovingElement element)
        {

            if (element is PlayerElement player)
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

    public class DoorBenDoorElement : DoorElement { }

}
