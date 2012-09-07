using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Epheremal.Model.Behaviours
{
    class MoveToward : Move
    {
        public override void apply(Character character)
        {

            double velocity = 0.75;

            double aggroRange = 100;

            //distance from player 
            double dx = character.PosX - Engine.Player.PosX;

            
                //player is right of character and within aggro range
                if (dx < 0 && dx > -aggroRange)
                {
                   
                    character.XVel = velocity;
                }
                //player is left of character and within aggro range
                else if (dx > 0 && dx < aggroRange)
                {
                    
                    character.XVel = -velocity;
                }
                else
                {
                    character.XVel = 0;
                }
          }
            
           
            
        
    }
}
