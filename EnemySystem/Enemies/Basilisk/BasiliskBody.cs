
namespace EnemySystem
{
    public class BasiliskBody : BasiliskSegment
    {
        public BasiliskBody(Basilisk basilisk, Point position) : base(basilisk, position, BASILISK_BODY_EI) { }

        protected override void WriteDeathActionText() { }
    }
}
