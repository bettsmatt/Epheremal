using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epheremal.Model
{
    class NPC : Character
    {
        public override Interactions.Interaction GetInteractionFor(Entity interactor)
        {
            throw new NotImplementedException();
        }
    }
}
