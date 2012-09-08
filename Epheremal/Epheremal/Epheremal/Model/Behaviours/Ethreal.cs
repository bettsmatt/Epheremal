﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Interactions;

namespace Epheremal.Model.Behaviours
{
    class Ethreal : Behaviour
    {
        public void apply(Character character)
        {
            //anti gravity
            character.YAcc -= 0.015;
        }
        public Interaction GetAppropriateInteractionFor(Character interactor, Entity interactee)
        {
            return null;
        }
    }
}
