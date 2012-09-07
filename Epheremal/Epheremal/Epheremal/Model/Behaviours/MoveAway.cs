using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epheremal.Model.Behaviours
{
    class MoveAway : Move
    {
        public override void apply(Character character)
        {
            double dx = character.PosX - Engine.Player.PosX;
            double dy = character.PosY - Engine.Player.PosY;

            character.XAcc += (1 / (7*dx));
            character.YAcc += (1 / (7*dy));

        }
    }
}
