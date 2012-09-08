using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epheremal.Model.Behaviours
{
    class MoveRight : Move
    {
        public override void apply(Character character)
        {
            character.XAcc += accelerationSpeed * (character is Player ? character.Jumping ? 6 : 6 : 1.5);
        }
    }
}
