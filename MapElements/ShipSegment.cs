

namespace MapElements
{
    public class ShipSegment : Element
    {
        private static ShipSegment _instance;
        private static bool _isInit = false;

        public static ShipSegment Instance
        {
            get
            {
                if (!_isInit)
                {
                    _instance = new ShipSegment();
                    _isInit = true;
                }

                return _instance;
            }
        }

        private ShipSegment() : base(EMPTY_EI) 
        {
            Background = SHIP_EBC;
        }
    }
}
