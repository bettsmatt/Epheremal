using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Interactions;

namespace Epheremal.Model.Behaviours
{
    public class Inanimate : Behaviour
    {

        public double firstX, firstY;
        bool first = true;

        public void apply(Character character)
        {
            // AntiGrav
            if (first)
            {
                firstX = character.PosX;
                firstY = character.PosY;
            }

            character.PosX = firstX;
            character.PosY = firstY;

            character.XVel = 0;
            character.YVel = 0;

            character.XAcc = 0;
            character.YAcc = 0;
        }
        public Interaction GetAppropriateInteractionFor(Character interactor, Entity interactee)
        {
            return null;
        }

    }
}
