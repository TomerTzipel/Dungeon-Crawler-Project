

class Program
{
    public static void Main(string[] args)
    {
        Printer.SetUp();
        LootManager.SetUp();
        SceneManager.SetUp();

        SceneManager.RunGame();  
    }
}