using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epheremal.Model.Behaviours
{
    class MoveAway : Move
    {
        public new void apply(Character character)
        {
            double dx = character.PosX - Engine.Player.PosX;
            double dy = character.PosY - Engine.Player.PosY;

            character.XAcc += 1 / dx;
            character.YAcc += 1 / dy;

        }
    }
}
