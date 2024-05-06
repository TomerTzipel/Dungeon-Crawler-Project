


namespace MapElements
{
    public class QuicksandTrap : Trap
    {

        public QuicksandTrap() : base() 
        {
            Background = QUICKSAND_EBC;
        }

        protected override void TrapEffect(Player element) 
        {
            element.Stun(10);
            Printer.AddActionText(ActionTextType.Stun, $"YOU FELL INTO QUICKSAND! WIGGLE OUT OF IT!");
        }

       



    }
}
