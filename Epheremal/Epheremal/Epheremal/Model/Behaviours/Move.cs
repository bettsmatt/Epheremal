using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Interactions;

namespace Epheremal.Model.Behaviours
{
    abstract class Move : Behaviour
    {
        protected double accelerationSpeed = 0.017 * Block.multToMatchBlock;
        public abstract void apply(Character character);
        public Interaction GetAppropriateInteractionFor(Character interactor, Entity interactee)
        {
            return null;
        }
    }
}
