

class Program
{
    public static void Main(string[] args)
    {
        GameManager.GameSetUp();
        bool playerStatus = true; 

        for (int i = 0; i < 10; i++) 
        {
            playerStatus = GameManager.GameLoop();

            if (!playerStatus) break;
        }
        //dfF
        Printer.Clear();

        if (playerStatus)
        {
            //W
            Console.WriteLine("You Win");
        }
        else
        {
            //L
            Console.WriteLine("Game Over");
        }
         
    }
}