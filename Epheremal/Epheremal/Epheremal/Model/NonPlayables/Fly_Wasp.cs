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
                                    new Flies(true),
                                    new MovePatrol((int)Block.multToMatchBlock * 30,(int)Block.multToMatchBlock * 30,(int)Block.multToMatchBlock * 1)
                                });
            this.Behaviours.Add(EntityState.BAD,
                                new List<Behaviour>()
                                {
                                    new Flies(true),
                                    new Deadly(),
                                    new MoveToward((int)Block.multToMatchBlock * 1.25,(int)Block.multToMatchBlock * 150,true,true),
                                }); 
        }


    }
}
