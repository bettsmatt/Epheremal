﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Behaviours;
using Epheremal.Model.Levels;

namespace Epheremal.Model.NonPlayables
{
    class Birdie : NPC
    {
        public Birdie(TileMap tileMap, int tileIDGood, int tileIDBad, int patrolLeft, int patrolRight) : base(tileMap, tileIDGood, tileIDBad)
        {
            this.Behaviours = new Dictionary<EntityState, List<Behaviour>>();
            this.Behaviours.Add(EntityState.GOOD,
                                new List<Behaviour>()
                                {
                                    new Harmless(),
                                    new Flies(),
                                    new MoveToward(),
                                    new MovePatrol( patrolLeft,  patrolRight)
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
