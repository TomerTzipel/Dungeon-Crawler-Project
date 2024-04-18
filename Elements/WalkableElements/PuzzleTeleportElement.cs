

namespace Elements
{
    public class PuzzleTeleportElement : WalkableElement
    {
        public PuzzleTeleportElement() : base(PUZZLE_EI) { }

        public override void WalkedOnEffect(MovingElement element)
        {
            if (element is PlayerElement player)
            {
                LevelManager.LoadRandomPuzzle();
                player.WalkOff();
            }
        }
    }
}
