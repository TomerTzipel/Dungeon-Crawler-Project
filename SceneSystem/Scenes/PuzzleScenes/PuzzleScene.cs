using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneSystem
{
    public class PuzzleScene : Scene
    {

        public PuzzleScene() : base(new GameInputManager()) { }

        public override void SceneLoop()
        {
            EnterScene();
        }

        protected override void EnterScene()
        {
            switch (LevelManager.CurrentLevel.PuzzleType)
            {
                case PuzzleType.Sudoku:
                    SceneManager.ChangeScene(SceneType.SudokuPuzzle);
                    return;
                case PuzzleType.Chess:
                    SceneManager.ChangeScene(SceneType.ChessPuzzle);
                    return;
                case PuzzleType.BreakingFloor:
                    SceneManager.ChangeScene(SceneType.SudokuPuzzle);
                    return;
            }

        }

        protected override void HandleInput() { }


        public override void PrintScene() { }







    }
}
