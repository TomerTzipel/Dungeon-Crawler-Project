


namespace PlayerSystem
{
    public static class PlayerManager
    {
        public static bool IsPlayerInit { get; private set; } = false;

        public static Player PlayerElement {  get; private set; }

        public static Inventory PlayerInventory 
        { 
            get
            {
                return PlayerElement.Inventory;
            }
        }
        public static PlayerCombatEntity CombatEntity
        {
            get
            {
                return PlayerElement.CombatEntity;
            }
        }
        public static void InitializePlayer()
        {
            if (!IsPlayerInit)
            {
                PlayerElement = new Player(new Point());
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
