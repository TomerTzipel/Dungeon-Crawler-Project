
using static System.Net.Mime.MediaTypeNames;

namespace Elements
{
    public class QuicksandTrapElement : TrapElement
    {

        public QuicksandTrapElement() : base() 
        {
            Background = QUICKSAND_EBC;
        }

        protected override void TrapEffect(PlayerElement element) 
        {
            element.Stun(10);
            Printer.AddActionText(ActionTextType.Stun, $"YOU FELL INTO QUICKSAND! WIGGLE OUT OF IT!");
        }

       



    }
}
