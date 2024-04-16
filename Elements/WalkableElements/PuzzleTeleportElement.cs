

namespace Elements
{
    public class PuzzleTeleportElement : WalkableElement
    {

        private Puzzle _puzzle;

        public PuzzleTeleportElement() : base(PUZZLE_EI) 
        {
            _puzzle = new SudokuPuzzle();
        }

        public override void WalkedOnEffect(MovingElement element)
        {
            if (element is PlayerElement player)
            {
                LevelManager.LoadRandomPuzzle(_puzzle);
                player.WalkOff();
                GameManager.PauseGame();
            }
        }
    }
}
