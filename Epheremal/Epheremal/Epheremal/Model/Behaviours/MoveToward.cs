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

            double acceleration = 0.01;

            double aggroRange = 100;

            //distance from player 
            double dx = character.PosX - Engine.Player.PosX;

            
                //player is right of character and within aggro range
                if (dx < 0 && dx > -aggroRange)
                {
                   
                    character.XAcc += acceleration;
                }
                //player is left of character and within aggro range
                else if (dx > 0 && dx < aggroRange)
                {
                    
                    character.XAcc -= acceleration;
                }
                else
                {
                    character.XAcc = 0;
                }
          }
            
           
            
        
    }
}
