﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Interactions;

namespace Epheremal.Model.Behaviours
{
    class Deadly : Behaviour
    {
        public void apply(Character character)
        {

        }
        public Interaction GetAppropriateInteractionFor(Character interactor, Entity interactee)
        {
            if (interactor.Behaviours[Entity.State].Exists(e => e is Unkillable)) return null;
            
            if (interactor is Player)
            return new PlayerDie(interactor, interactee);

            return new Die(interactor, interactee);
        }
    }
}
