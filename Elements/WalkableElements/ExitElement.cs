

namespace Elements
{
    internal class ExitElement : WalkableElement
    {
        public ExitElement() : base(EXIT_EI) { }
        public override void WalkedOnEffect(MovingElement element) 
        {
            if(element is PlayerElement player)
            {
                Printer.AddActionText(ActionTextType.Item, "A reward for finishing the level:");
                LootManager.RewardRandomItem();
                player.DidEnterExit = true;

                if((LevelManager.CurrentLevelValue + 1) % 2 == 0 & LevelManager.CurrentLevelValue != -1)
                {
                    GameManager.IsShopAvailable = true;
                }
            }
        }
    }
}
