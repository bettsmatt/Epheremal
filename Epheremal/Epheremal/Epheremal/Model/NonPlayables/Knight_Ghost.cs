using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Levels;
using Epheremal.Model.Behaviours;

namespace Epheremal.Model.NonPlayables
{
    class Knight_Ghost : NPC
    {
        public Knight_Ghost(TileMap tileMap, int tileIDGood, int tileIDBad)
            : base(tileMap, tileIDGood, tileIDBad)
        {
            this.Behaviours = new Dictionary<EntityState, List<Behaviour>>();

            this.Behaviours.Add(EntityState.GOOD,
                                new List<Behaviour>()
                                {
                                    new MovePatrol(3,3,1),
                                    new Deadly()   
                                });
            this.Behaviours.Add(EntityState.BAD,
                                new List<Behaviour>()
                                {
                                    new Adhesive(),
                                    new MovePatrol(3,3,1)
                                });
        }


    }
}
