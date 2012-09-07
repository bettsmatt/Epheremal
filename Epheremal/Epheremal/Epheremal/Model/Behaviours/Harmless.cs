using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Interactions;

namespace Epheremal.Model.Behaviours
{
    class Harmless : Behaviour
    {
        public void apply(Character character)
        {

        }

        public Interaction GetAppropriateInteractionFor(Character interactor, Entity interactee)
        {
            if (interactor is Player)
                return new Collide(interactor, interactee);
            else return null;
        }
    }
}
