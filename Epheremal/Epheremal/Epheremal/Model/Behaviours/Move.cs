﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epheremal.Model.Behaviours
{
    abstract class Move : Behaviour
    {
        public abstract void apply(Character character);
    }
}
