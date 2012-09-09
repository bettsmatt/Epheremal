using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Levels;
using Epheremal.Model.Behaviours;

namespace Epheremal.Model.NonPlayables
{
    class Snail_Bull : NPC
    {
        public Snail_Bull(TileMap tileMap, int tileIDGood, int tileIDBad)
            : base(tileMap, tileIDGood, tileIDBad)
        {
            this.Behaviours = new Dictionary<EntityState, List<Behaviour>>();

            this.Behaviours.Add(EntityState.GOOD,
                                new List<Behaviour>()
                                {
                                    new Deadly(),
                                    new MovePatrol((int)Block.multToMatchBlock * 30,(int)Block.multToMatchBlock * 30, (int)Block.multToMatchBlock * 0.1f)
                                });
            this.Behaviours.Add(EntityState.BAD,
                                new List<Behaviour>()
                                {
                                    new Deadly(),
                                    new MoveToward((int)Block.multToMatchBlock * 2,(int)Block.multToMatchBlock * 200,true,false),
                                }); 
        }


    }
}
