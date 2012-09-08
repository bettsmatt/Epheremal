using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epheremal.Model.Interactions
{
    class Boost : InteractionBase
    {
        public Boost(Character a, Entity b)
            : base(a, b)
        {}

        public override void Interact()
        {
            applyTo(Interactor, Interactee);
            if(Interactee is Character) applyTo((Character)Interactee, Interactor);
        }

        public void applyTo(Character Interactor, Entity Interactee)
        {
            Interactor.XAcc *= 5;
        }

    }
}
