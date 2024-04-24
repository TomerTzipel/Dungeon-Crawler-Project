


namespace GameSystems
{
    //need to seperate MovingElement from PlayerManager
    public static class PlayerManager
    {
        private static bool _isPlyaerInit = false;

        public static PlayerElement PlayerElement {  get; private set; }

        public static Inventory PlayerInventory 
        { 
            get
            {
                return PlayerElement.Inventory;
            }
        }

        public static void InitializePlayer()
        {
            if (!_isPlyaerInit)
            {
                PlayerElement = new PlayerElement(new Point());
                _isPlyaerInit = true;
            }
        }

        public static void ClearPlayer()
        {
            PlayerElement = null;
            _isPlyaerInit = false;
        }

        public static void LocalizePlayerInLevel(Point startPosition)
        {
            PlayerElement.InitializePositionOnNewLevel(startPosition);
        }
    }
}
