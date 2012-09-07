using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epheremal.Model.Behaviours
{
    class Jumps : Behaviour
    {
        //jump vlocity magic number
        int jumpVelocity = 20;

        public void apply(Character character)
        {
            
            //if charaters current vertical velocity is 0 do jump
            if (character.YVel == 0)
            {
                //add a positive vertical velocity
                character.YVel += jumpVelocity;
            }
                
            //else do nothing
        }
    }
}
