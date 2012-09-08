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

            double acceleration = 0.1;

            if (Math.Abs(character.PosX - Engine.Player.PosX) < _aggroRange && Math.Abs(character.PosY - Engine.Player.PosY) < _aggroRange)
            {

                if (_doX)
                {

                    if (Math.Abs(character.PosX - Engine.Player.PosX) < _aggroRange)
                    {

                        if (character.PosX < Engine.Player.PosX)
                            character.XAcc += acceleration * _speedMod;
                        if (character.PosX > Engine.Player.PosX)
                            character.XAcc -= acceleration * _speedMod;
                    }
                }
                if (_doY)
                {
                    if (Math.Abs(character.PosY - Engine.Player.PosY) < _aggroRange)
                    {
                        if (character.PosY > Engine.Player.PosY)
                            character.YAcc -= acceleration * _speedMod;

                        if (character.PosY < Engine.Player.PosY)
                            character.YAcc += acceleration * _speedMod;
                    }
                }
            }

                /*
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
                */
          }
            
    }
}
