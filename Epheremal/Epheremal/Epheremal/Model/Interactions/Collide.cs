using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epheremal.Model.Interactions
{
    class Collide : InteractionBase
    {
        public Collide(Character a, Entity b)
            : base(a, b)
        {

        }

        public override void Interact()
        {
            this.Interactor.YVel = 0;
            this.Interactor.YAcc = -0.3* this.Interactor.YAcc; //bounce a little
        }
    }
}
