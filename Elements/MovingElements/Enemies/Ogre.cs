﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elements
{
    public class Ogre : EnemyElement
    {
        private int _stunDuration = 3;

        public Ogre(Point position) : base(position, OGRE_EI, 
            new CombatEntity(OGRE_HP, OGRE_DAMAGE, OGRE_EVASION, OGRE_ACCURACY, OGRE_MULTIHIT,OGRE_ARMOR,OGRE_PIERCE))
        {
            _movementSpeed = OGRE_MOVEMENT_SPEED;
            _range = OGRE_RANGE;
        }

        public override bool CollideWith(MovingElement collidor, Map map)
        {
            if (collidor is PlayerElement player)
            {
                bool wasPlayerHit = player.GetAttacked(CombatEntity);

                if (wasPlayerHit)
                {
                    player.Stun(_stunDuration);
                    Debug.WriteLine("Stunned");
                }
                
                if (!player.CombatEntity.IsAlive)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
