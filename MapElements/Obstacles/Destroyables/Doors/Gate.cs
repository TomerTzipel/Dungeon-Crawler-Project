

namespace MapElements
{
    public class Gate : Door
    {
        private static Gate _instance;
        private static bool _isInit = false;

        public static Gate Instance
        {
            get 
            {
                if (!_isInit)
                {
                    _instance = new Gate();
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
