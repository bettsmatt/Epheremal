using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epheremal.Model.Interactions
{
    class Collect : InteractionBase
    {

        Character player;
        Entity entity;


        public Collect(Character a, Entity b)
            : base(a, b)
        {
            player = a;
            entity = b;
        }

        public override void Interact()
        {
            if (player is Player)
            {
                //can add checks here for entity types to determine the point or life value
                ((Player)player).score += 100; 
            }
        }

        
    }
}
