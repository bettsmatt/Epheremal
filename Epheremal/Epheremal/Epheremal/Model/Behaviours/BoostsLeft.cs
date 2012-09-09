using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Interactions;

namespace Epheremal.Model.Behaviours
{
    class BoostsLeft : Behaviour
    {
        public void apply(Character character)
        {

        }
        public Interaction GetAppropriateInteractionFor(Character interactor, Entity interactee)
        {
            Boost b = new Interactions.Boost(interactor, interactee);
            b.IsLeft(true);
            return b;
        }
    }
}
