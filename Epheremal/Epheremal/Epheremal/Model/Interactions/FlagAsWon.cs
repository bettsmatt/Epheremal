using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epheremal.Model.Interactions
{
    class FlagAsWon : InteractionBase
    {

        Character player;
        Entity entity;


        public FlagAsWon(Character a, Entity b)
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
                ((Player)player).score += 1000;
                //SoundEffects.sounds["win"].Play();
                Engine.triggetNextLevel = true;
                new Die((Character)entity, player).Interact();
            }
        }

        
    }
}
