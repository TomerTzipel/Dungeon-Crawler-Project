using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elements
{
    public class BasiliskBody : BasiliskSegment
    {
        public BasiliskBody(Basilisk basilisk, Point position) : base(basilisk, position, BASILISK_BODY_EI) { }

        protected override void WriteDeathActionText() { }
    }
}
