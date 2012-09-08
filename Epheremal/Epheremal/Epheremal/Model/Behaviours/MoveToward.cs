using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Epheremal.Model.Behaviours
{
    class MoveToward : Move
    {

        double _speedMod;
        double _aggroRange;
        bool _doX;
        bool _doY;

        public MoveToward(double speedMod, double aggroRange, bool dox, bool doy){
        
            _doX = dox;
            _doY = doy;
            _speedMod = speedMod;
            _aggroRange = aggroRange;
        }

        public override void apply(Character character)
        {

            double acceleration = 1;

            if (_doX)
            {

                //distance from player 
                double dx = character.PosX - Engine.Player.PosX;


                //player is right of character and within aggro range
                if (dx < 0 && dx > -_aggroRange)
                {

                    character.PosX += acceleration * _speedMod;
                }
                //player is left of character and within aggro range
                else if (dx > 0 && dx < _aggroRange)
                {

                    character.PosX -= acceleration * _speedMod;
                }
                else
                {
                    //character.XAcc = 0;
                }
            }

            if (_doY) {
                //distance from player 
                double dy = character.PosY - Engine.Player.PosY;


                //player is right of character and within aggro range
                if (dy < 0 && dy > -_aggroRange)
                {

                    character.PosY += acceleration * _speedMod;
                }
                //player is left of character and within aggro range
                else if (dy > 0 && dy < _aggroRange)
                {

                    character.PosY -= acceleration * _speedMod;
                }
                else
                {
                  //  character.YAcc = 0;
                }
            }

          }
            
           
            
        
    }
}
