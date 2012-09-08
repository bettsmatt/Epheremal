using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Interactions;
using Microsoft.Xna.Framework.Audio;

namespace Epheremal.Model.Behaviours
{
    class Jumps : Behaviour
    {
        //jump vlocity magic number
        double jumpAcceleration = Character.ABS_TERMINAL_VELOCITY_Y/4;

        

        public void apply(Character character)
        {
            
            //if charaters current vertical velocity is 0 do jump
            if (Math.Abs(character.YVel) < 0.3 && !character.Jumping)
            {
                character.Jumping = !character.Jumping;
                character.Jumping = true;
                //add a positive vertical velocity
                character.YAcc -= jumpAcceleration;
                if (SoundEffects.sounds["jump"].State == SoundState.Stopped)
                {
                    SoundEffects.sounds["jump"].Volume = 0.75f;
                    // soundInstance.IsLooped = False;
                    SoundEffects.sounds["jump"].Play();
                }
            }
                
            //else do nothing
        }

        public Interaction GetAppropriateInteractionFor(Character interactor, Entity interactee)
        {
            return null;
        }
    }
}
