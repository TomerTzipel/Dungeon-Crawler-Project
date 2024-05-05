

using System.Diagnostics;

namespace Elements
{
    internal class ExitElement : WalkableElement
    {
        public ExitElement() : base(EXIT_EI) { }
        public override void WalkedOnEffect(MovingElement element) 
        {
            
            if (element is PlayerElement player)
            {

                if (!EnemyManager.Instance.IsEmpty && !Settings.DisableExitPrerequisite)
                {
                    Printer.AddActionText(ActionTextType.General, $"There are still {EnemyManager.Instance.Count} enemies...");
                    return;
                }

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
