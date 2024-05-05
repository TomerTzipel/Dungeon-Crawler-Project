using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elements
{
    public class GateElement : DoorElement
    {
        private static GateElement _instance;
        private static bool _isInit = false;

        public static GateElement Instance
        {
            get 
            {
                if (!_isInit)
                {
                    _instance = new GateElement();
                    _isInit = true;
                }

                return _instance; 
            }
        }

        public override bool HitBy(MovingElement element) 
        {
            if (_isOpened)
            {
                return false;
            }

            return true;
        }


        public void Open()
        {
            _isOpened = true;
            _isInit = false;
        }


    }
}
