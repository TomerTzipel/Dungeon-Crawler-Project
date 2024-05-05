using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Elements
{
    public class BasiliskTail : BasiliskSegment
    {
        public BasiliskTail(Basilisk basilisk,Point position) : base(basilisk, position,BASILISK_TAIL_EI) { }

        public bool TailSwipe()
        {
           if(DistanceToPlayer() == 1)
            {
                PlayerManager.PlayerElement.GetAttacked(CombatEntity);
                return true;
            }

            return false;
        }

        protected override void WriteDeathActionText() { }
    }
}
