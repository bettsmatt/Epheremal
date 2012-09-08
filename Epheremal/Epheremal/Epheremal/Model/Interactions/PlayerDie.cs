using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Epheremal.Model.Interactions
{
    class PlayerDie : InteractionBase
    {

        Character player;
        Entity entity;

        public PlayerDie(Character a, Entity b)
            : base(a, b)
        {
            player = a;
            entity = b;
        }

       

        public override void Interact()
        {
            if (player is Player && ((Player)player).isDead != true)
            {                
                ((Player)player).isDead = true;
                ((Player)player).lives--;
            }
        }
    }
}
