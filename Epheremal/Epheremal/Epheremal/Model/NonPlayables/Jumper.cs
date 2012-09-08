using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Behaviours;
using Epheremal.Model.Levels;

namespace Epheremal.Model.NonPlayables
{
    class Jumper : NPC
    {
        public Jumper(TileMap tileMap, int tileIDGood, int tileIDBad) : base(tileMap, tileIDGood, tileIDBad)
        {
            this.Behaviours = new Dictionary<EntityState, List<Behaviour>>();
            this.Behaviours.Add(EntityState.GOOD,
                                new List<Behaviour>()
                                {
                                    new Harmless(),
                                    new Jumps(),
                                    new MoveToward(),
                                });
            this.Behaviours.Add(EntityState.BAD,
                                new List<Behaviour>()
                                {
                                    new Deadly(),
                                    new Jumps(),
                                    new MoveToward(),
                                }); 
        }

    }
}
