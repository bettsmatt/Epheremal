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
            this.Interactor.YAcc = -1 * this.Interactor.YAcc; //bounce a bunch
        }
    }
}
