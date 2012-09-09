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
    class Boss : NPC
    {
        public Boss(TileMap tileMap, int tileIDGood, int tileIDBad)
            : base(tileMap, tileIDGood, tileIDBad)
        {

            this.Behaviours = new Dictionary<EntityState, List<Behaviour>>();

            this.Behaviours.Add(EntityState.GOOD,
                                new List<Behaviour>()
                                {
                                    new Deadly(),
                                    new Flies(true),
                                    new MovePatrolVert(30,1)
                                });


            this.Behaviours.Add(EntityState.BAD,
                                new List<Behaviour>()
                                {
                                    new Inanimate(),
                                    new Harmless()
                                });
            _width *= 3;
            _height *= 3;
        }

        public override Rectangle GetBoundingRectangle()
        {
            int xPosition = Convert.ToInt32(PosX - Engine.xOffset), yPosition = Convert.ToInt32(PosY - Engine.yOffset);
            return new Rectangle(xPosition, yPosition, this._width, this._height);
        }

        public override SpriteBatch RenderSelf(ref SpriteBatch sprites)
        {
            Color tint = Color.White;

            if (Entity.State == EntityState.GOOD)
                if (XVel < 0)
                    sprites.Draw(this._tileMap.TileMapTexture, this.GetBoundingRectangle(), _tileMap.getRectForTile(_tileIDGood), tint);
                else
                    sprites.Draw(this._tileMap.TileMapTexture, this.GetBoundingRectangle(), _tileMap.getRectForTile(_tileIDGood + 1), tint);

            else
                if (XVel < 0)
                    sprites.Draw(this._tileMap.TileMapTexture, this.GetBoundingRectangle(), _tileMap.getRectForTile(_tileIDBad), tint);
                else
                    sprites.Draw(this._tileMap.TileMapTexture, this.GetBoundingRectangle(), _tileMap.getRectForTile(_tileIDBad + 1), tint);
            
            
            return sprites;
        }

    }
}
