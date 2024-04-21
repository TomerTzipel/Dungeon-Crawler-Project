

namespace Elements
{
    public class Bat : EnemyElement
    {
        private bool isUpState = false;

        public Bat(Point position) : base(position, BAT_DDWN_EI, 
            new CombatEntity(BAT_HP,BAT_DAMAGE,BAT_EVASION,BAT_ACCURACY,BAT_MULTIHIT,BAT_ARMOR,BAT_PIERCE)) 
        { 
            _movementSpeed = BAT_MOVEMENT_SPEED;
            _range = BAT_RANGE;
        }
        public override bool Tick(Map map, float interval)
        {
            _movementCounter += interval;

            if (_movementCounter >= _movementSpeed)
            {
                _movementCounter = 0f;

                Direction direction = CalculateMovement();
                map.MoveElementInDirection(this, direction);
                return true;
            }

            return false;
        }
        public void ChangeState()
        {
            if (isUpState)
            {
                Identifier = BAT_DDWN_EI;
                isUpState = false;
            }
            else
            {
                Identifier = BAT_UP_EI;
                isUpState = true;
            }
        } 

        protected override void WriteDeathActionText()
        {
            Printer.AddActionText(ActionTextType.CombatPositive, "You killed a bat!");
        }
    }
}
