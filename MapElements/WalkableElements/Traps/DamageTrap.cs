
namespace MapElements
{
    public class DamageTrap : Trap
    {

        public DamageTrap() : base()
        {
            Background = BLOOD_PUDDLE_EBC;
        }
        protected override void TrapEffect(Player player)
        {
            player.LoseHpByMaxHpPrecentage(10);
        }
    }
}
