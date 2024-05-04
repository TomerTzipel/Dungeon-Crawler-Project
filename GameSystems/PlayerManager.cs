


namespace GameSystems
{
    //need to seperate MovingElement from PlayerManager
    public static class PlayerManager
    {
        public static bool IsPlayerInit { get; private set; } = false;

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
            if (!IsPlayerInit)
            {
                PlayerElement = new PlayerElement(new Point());
                IsPlayerInit = true;
            }
        }

        public static void ClearPlayer()
        {
            PlayerElement = null;
            IsPlayerInit = false;
        }

        public static void LocalizePlayerInLevel(Point startPosition)
        {
            PlayerElement.InitializePositionOnNewLevel(startPosition);
        }
    }
}
