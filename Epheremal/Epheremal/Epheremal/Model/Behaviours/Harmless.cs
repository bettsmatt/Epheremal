using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Interactions;
using System.Diagnostics;

namespace Epheremal.Model.Behaviours
{
    class Harmless : Behaviour
    {
        public void apply(Character character)
        {

        }

        public Interaction GetAppropriateInteractionFor(Character interactor, Entity interactee)
        {
            return new Collide(interactor, interactee);
        }
    }
}
