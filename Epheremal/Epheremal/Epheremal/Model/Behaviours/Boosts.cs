using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Interactions;

namespace Epheremal.Model.Behaviours
{
    class Boosts : Behaviour
    {
        public void apply(Character character)
        {

        }
        public Interaction GetAppropriateInteractionFor(Character interactor, Entity interactee)
        {
            return new Interactions.Boost(interactor, interactee);
        }
    }
}
