﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Behaviours;

namespace Epheremal.Model.NonPlayables
{
    class Charger : NPC
    {
        public Charger()
        {
            this.Behaviours = new Dictionary<EntityState, List<Behaviour>>();
            this.Behaviours.Add(EntityState.GOOD,
                                new List<Behaviour>()
                                {
                                    new Harmless(),
                                    new MoveToward(),
                                });
            this.Behaviours.Add(EntityState.BAD,
                                new List<Behaviour>()
                                {
                                    new Deadly(),
                                    new MoveToward(),
                                }); 
        }

    }
}