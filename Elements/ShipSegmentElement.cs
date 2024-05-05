using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elements
{
    public class ShipSegmentElement : Element
    {
        private static ShipSegmentElement _instance;
        private static bool _isInit = false;

        public static ShipSegmentElement Instance
        {
            get
            {
                if (!_isInit)
                {
                    _instance = new ShipSegmentElement();
                    _isInit = true;
                }

                return _instance;
            }
        }

        private ShipSegmentElement() : base(EMPTY_EI) 
        {
            Background = SHIP_EBC;
        }
    }
}
