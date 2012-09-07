using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epheremal.Model.Interactions
{
    public abstract class InteractionBase : Interaction
    {
        Entity Interactor, Interactee;
        public InteractionBase(Entity interactor, Entity interactee)
        {
            this.Interactor = interactor;
            this.Interactee = interactee;
        }

        public abstract void Interact();
    }
}
