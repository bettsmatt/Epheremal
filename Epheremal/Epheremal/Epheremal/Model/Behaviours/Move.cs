using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epheremal.Model.Behaviours
{
    abstract class Move : Behaviour
    {
        protected double accelerationSpeed = 0.01;
        public abstract void apply(Character character);
    }
}
