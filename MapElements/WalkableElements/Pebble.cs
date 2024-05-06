

namespace MapElements
{
    public class Pebble : WalkableElement
    {
        public Pebble() : base(PEBBLE_EI)
        {
            Foreground = PEBBLE_EFC;
        }

        public override void WalkedOnEffect(MovingElement element)
        {
            if (element is Bat)
            {
                return;
            }

            element.WalkOff();

            if(Background == BLOOD_PUDDLE_EBC)
            {
                element.WalkOn(new WalkableElement(BLOOD_PUDDLE_EBC, EMPTY_EI));
            }
        }
    }
}
