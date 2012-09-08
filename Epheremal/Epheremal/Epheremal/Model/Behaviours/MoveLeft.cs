using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epheremal.Model.Behaviours
{
    class MoveLeft : Move
    {
        public override void apply(Character character)
        {            
            character.XAcc -= accelerationSpeed * (character is Player ? character.Jumping ? 4 : 8 : 1);
        }
    }
}
