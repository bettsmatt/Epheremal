using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epheremal.Model.Behaviours
{
    class MoveToward : Move
    {
        public override void apply(Character character)
        {

            double velocity = 10;

            double aggroRange = 10;

            //distance from player 
            double dx = character.PosX - Engine.Player.PosX;

            
                //player is left of character and within aggro range
                if (dx < 0 || dx > -aggroRange)
                {
                    character.XAcc = -velocity;
                }
                //player is right of character and within aggro range
                else if (dx > 0 || dx < aggroRange)
                {
                    character.XAcc = velocity;
                }
          }
            
           
            
        
    }
}
