using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Levels;
using Epheremal.Model.Behaviours;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Epheremal.Model.NonPlayables
{
    class Coin : NPC
    {
        public Coin(TileMap tileMap, int tileIDGood, int tileIDBad)
            : base(tileMap, tileIDGood, tileIDBad)
        {
            this.Behaviours = new Dictionary<EntityState, List<Behaviour>>();

            this.Behaviours.Add(EntityState.GOOD,
                                new List<Behaviour>()
                                {
                                    new Collectable(),
                                    new Inanimate()
                                });
            this.Behaviours.Add(EntityState.BAD,
                                new List<Behaviour>()
                                {
                                    new Collectable(),
                                    new Inanimate()
                                });
        }
        
        public override SpriteBatch RenderSelf(ref SpriteBatch sprites)
        {
            sprites.Draw(this._tileMap.TileMapTexture, this.GetBoundingRectangle(), _tileMap.getRectForTile(_tileIDGood + AnimatedTexture.Frame), Color.White);
            return sprites;
        }
    }
}
