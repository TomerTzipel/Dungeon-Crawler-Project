


namespace SceneSystem
{
    public class GameOverMenuScene : SelectionMenuScene
    {
        public GameOverMenuScene() : base(1,"",ConsoleColor.White) 
        {
            _buttons[0] = new ChangeSceneButton("Main Menu", SceneType.MainMenu);
        }

        public override void PrintScene()
        {
            Console.SetCursorPosition(0, 0);

            if (GameManager.GameResult)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("YOU WIN!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("YOU DIED!");
            }
            Printer.ColorReset();
            Console.WriteLine();

            for (int i = 0; i < _buttons.Length; i++)
            {
                Console.WriteLine(_buttons[i]);
                Printer.ColorReset();
                Console.WriteLine();
            }

        }
    }
}
