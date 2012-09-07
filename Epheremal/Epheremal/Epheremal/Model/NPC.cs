using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epheremal.Model
{
    class NPC : Character
    {
        public override Interactions.Interaction[] GetInteractionsFor(Character interactor)
        {
            throw new NotImplementedException();
        }
    }
}
