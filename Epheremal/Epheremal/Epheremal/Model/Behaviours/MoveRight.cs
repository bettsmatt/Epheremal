using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epheremal.Model.Behaviours
{
    class MoveRight : Move
    {

        float _speedMod;
        public MoveRight(float speedMod) {
            _speedMod = speedMod;
        }

        public override void apply(Character character)
        {
            if(Engine.MarioControl)
                character.XAcc += accelerationSpeed * (character is Player ? character.Jumping ? 8 : 8 : 1 * _speedMod);
            else
                character.XAcc += accelerationSpeed * (character is Player ? character.Jumping ? 6 : 6 : 1.5);
            
        }
    }
}
