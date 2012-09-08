using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Behaviours;

namespace Epheremal.Model.NonPlayables
{
    class Ghost : NPC
    {
        public Ghost(int patrolLeft, int patrolRight)
        {
            this.Behaviours = new Dictionary<EntityState, List<Behaviour>>();
            this.Behaviours.Add(EntityState.GOOD,
                                new List<Behaviour>()
                                {
                                    new Ethreal(),
                                    new MovePatrol( patrolLeft,  patrolRight)
                                });
            this.Behaviours.Add(EntityState.BAD,
                                new List<Behaviour>()
                                {
                                    new Deadly(),
                                    new MovePatrol( patrolLeft,  patrolRight)
                                }); 
        }

    }
}
