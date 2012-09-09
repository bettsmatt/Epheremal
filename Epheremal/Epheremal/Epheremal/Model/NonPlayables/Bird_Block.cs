using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Levels;
using Epheremal.Model.Behaviours;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Epheremal.Model.NonPlayables
{
    class Bird_Block : NPC
    {
        public Bird_Block(TileMap tileMap, int tileIDGood, int tileIDBad, bool vert)
            : base(tileMap, tileIDGood, tileIDBad)
        {

            this.Behaviours = new Dictionary<EntityState, List<Behaviour>>();

            if (vert)
            {
                this.Behaviours.Add(EntityState.GOOD,
                                    new List<Behaviour>()
                                {
                                    new Deadly(),
                                    new Flies(true),
                                    new MovePatrolVert(30 * (int)Block.multToMatchBlock, 1 * (int)Block.multToMatchBlock)
                                });
            }
            else { 
                                this.Behaviours.Add(EntityState.GOOD,
                                    new List<Behaviour>()
                                {
                                    new Deadly(),
                                    new Flies(true),
                                    new MovePatrol( (int)Block.multToMatchBlock * 30,(int)Block.multToMatchBlock * 30,(int)Block.multToMatchBlock * 1)
                                });
            }
            this.Behaviours.Add(EntityState.BAD,
                                new List<Behaviour>()
                                {
                                    new Inanimate(),
                                    new Harmless()
                                });
        }

    }
}
