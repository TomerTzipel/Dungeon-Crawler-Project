

namespace MapElements
{
    public class PuzzleTeleport : WalkableElement
    {
        public PuzzleTeleport() : base(PUZZLE_EI, PUZZLE_EFC) { }

        public override void WalkedOnEffect(MovingElement element)
        {
            if (element is Player player)
            {
                LevelManager.LoadRandomPuzzle();
                player.WalkOff();
            }
        }
    }
}
