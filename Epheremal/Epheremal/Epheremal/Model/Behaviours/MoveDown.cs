using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epheremal.Model.Behaviours
{
    class MoveDown : Move
    {

        float _speedMod;

        public MoveDown(float speedMod)
        {
            _speedMod = speedMod;
        }

        public override void apply(Character character)
        {
            if(Engine.MarioControl)
                character.YAcc += accelerationSpeed * (character is Player ? character.Jumping ? 4 : 8 : 1 * _speedMod);
            else
                character.YAcc += accelerationSpeed * (character is Player ? character.Jumping ? 6 : 6 : 1.5);
        }

    }
}
