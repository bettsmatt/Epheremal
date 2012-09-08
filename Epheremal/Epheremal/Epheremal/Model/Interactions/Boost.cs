using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epheremal.Model.Interactions
{
    class Boost : InteractionBase
    {
        private bool _isLeft;

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
            int vel = 20;
            int acc = 8;
            if (_isLeft)
            {
                Interactor.XVel = vel;
                Interactor.XAcc = acc;
            }
            else
            {
                Interactor.XVel = -vel;
                Interactor.XAcc = -acc;
            }
        }

        public void IsLeft(bool maybe)
        {
            _isLeft = maybe;
        }

    }
}
