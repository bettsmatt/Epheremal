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
                ((Player)player).AddScore(100);
                SoundEffects.sounds["pickupcoin"].Volume = 0.25f;
                SoundEffects.sounds["pickupcoin"].Play();
                new Die((Character)entity, player).Interact();
            }
        }

        
    }
}
