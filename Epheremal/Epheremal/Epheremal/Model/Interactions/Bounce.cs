using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epheremal.Model.Interactions
{
    class Bounce : InteractionBase
    {
        public Bounce(Character a, Entity b)
            : base(a, b)
        {}

        public override void Interact()
        {
            applyTo(Interactor, Interactee);
            if(Interactee is Character) applyTo((Character)Interactee, Interactor);
        }

        public void applyTo(Character Interactor, Entity Interactee)
        {
            double dx = Interactor.GetX() - Interactee.GetX();
            double dy = Interactor.GetY() - Interactee.GetY();
            double yVel = ((Character)Interactor).YVel;
            double xVel = ((Character)Interactor).XVel;

           
        }
    }
}
