using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epheremal.Model.Behaviours
{
    class MoveRight : Move
    {
        public new void apply(Character character)
        {
            character.XAcc += accelerationSpeed;
        }
    }
}
