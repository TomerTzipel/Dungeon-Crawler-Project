
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
        }

       



    }
}
