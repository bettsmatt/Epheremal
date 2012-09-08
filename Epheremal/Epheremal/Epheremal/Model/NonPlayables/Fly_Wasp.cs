using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Behaviours;
using Epheremal.Model.Levels;

namespace Epheremal.Model.NonPlayables
{
    class Fly_Wasp : NPC
    {
        public Fly_Wasp(TileMap tileMap, int tileIDGood, int tileIDBad)
            : base(tileMap, tileIDGood, tileIDBad)
        {
            this.Behaviours = new Dictionary<EntityState, List<Behaviour>>();
            this.Behaviours.Add(EntityState.GOOD,
                                new List<Behaviour>()
                                {
                                    new Harmless(),
                                    new Flies(),
                                    new MovePatrol(30,30,1)
                                });
            this.Behaviours.Add(EntityState.BAD,
                                new List<Behaviour>()
                                {
                                    new Flies(),
                                    new Deadly(),
                                    new MoveToward(2,100,true,true),
                                }); 
        }


    }
}
