using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epheremal.Model.Interactions
{
    class Stick : InteractionBase
    {
        public Stick(Character a, Entity b)
            : base(a, b)
        {}

        public override void Interact()
        {
            this.Interactor.YAcc = 0.5* this.Interactor.YAcc; //bounce a little
        }
    }
}
