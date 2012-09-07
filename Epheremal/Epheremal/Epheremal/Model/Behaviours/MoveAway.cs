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

            double xDelta = (1 / (7 * dx));
            double yDelta = (1 / (7 * dy));

            if (xDelta > 0.3) xDelta = 0.3;
            if (xDelta < -0.3) xDelta = -0.3;

            if (yDelta > 0.3) yDelta = 0.3;
            if (yDelta < -0.3) yDelta = -0.3;

            character.XAcc += xDelta;
            character.YAcc += yDelta;

        }
    }
}
