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
            character.XAcc -= accelerationSpeed * (character is Player ? 0.5 : 1);
        }
    }
}
