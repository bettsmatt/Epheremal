using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Interactions;

namespace Epheremal.Model.Behaviours
{
    public interface Behaviour
    {
        void apply(Character toChar);
        Interaction GetAppropriateInteractionFor(Character interactor, Entity Interactee);
    }
}
