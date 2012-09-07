using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epheremal.Model.Behaviours
{
    class Jumps : Behaviour
    {
        //jump vlocity magic number
        double jumpAcceleration = Character.ABS_TERMINAL_VELOCITY/5;

        public void apply(Character character)
        {
            
            //if charaters current vertical velocity is 0 do jump
            if (character.YVel == 0)
            {
                //add a positive vertical velocity
                character.YAcc -= jumpAcceleration;
            }
                
            //else do nothing
        }
    }
}
