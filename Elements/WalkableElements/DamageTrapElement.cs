
namespace Elements
{
    public class DamageTrapElement : TrapElement
    {

        public DamageTrapElement() : base()
        {
            Background = BLOOD_PUDDLE_EBC;
        }
        protected override void TrapEffect(PlayerElement player)
        {
            player.LoseHpByMaxHpPrecentage(10);
        }
    }
}
