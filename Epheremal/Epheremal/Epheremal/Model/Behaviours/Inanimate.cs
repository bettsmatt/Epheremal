using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Interactions;

namespace Epheremal.Model.Behaviours
{
    public class Inanimate : Behaviour
    {
        public void apply(Character character)
        {
            // AntiGrav



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
