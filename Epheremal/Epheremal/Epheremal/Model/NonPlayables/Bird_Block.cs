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
        public Bird_Block(TileMap tileMap, int tileIDGood, int tileIDBad)
            : base(tileMap, tileIDGood, tileIDBad)
        {
            this.Behaviours = new Dictionary<EntityState, List<Behaviour>>();

            this.Behaviours.Add(EntityState.GOOD,
                                new List<Behaviour>()
                                {
                                    new Deadly(),
                                    new Flies(true),
                                    new MovePatrol(30,30,1)
                                });
            this.Behaviours.Add(EntityState.BAD,
                                new List<Behaviour>()
                                {
                                    new Inanimate(),
                                    new Harmless()
                                });
        }

        public override Rectangle GetBoundingRectangle()
        {

            if (Entity.State == EntityState.BAD)
            {
                int xPosition = Convert.ToInt32(PosX - Engine.xOffset), yPosition = Convert.ToInt32(PosY - Engine.yOffset);

                return new Rectangle(xPosition - (int)(Block.BLOCK_WIDTH), yPosition,  Block.BLOCK_WIDTH * 3, this._height);
            }

            else 
                return base.GetBoundingRectangle();
        }

        public override SpriteBatch RenderSelf(ref SpriteBatch sprites)
        {
            if (Dead) return sprites;
            Color tint = Engine.Alert ? Color.Red : Color.White;

            {
                if (Entity.State == EntityState.GOOD)
                    if (XVel < 0)
                        sprites.Draw(this._tileMap.TileMapTexture, this.GetBoundingRectangle(), _tileMap.getRectForTile(_tileIDGood), tint);
                    else
                        sprites.Draw(this._tileMap.TileMapTexture, this.GetBoundingRectangle(), _tileMap.getRectForTile(_tileIDGood + 1), tint);

                else{

                    int xPosition = Convert.ToInt32(PosX - Engine.xOffset), yPosition = Convert.ToInt32(PosY - Engine.yOffset);

                        sprites.Draw(this._tileMap.TileMapTexture, new Rectangle(
                            xPosition,
                            yPosition,
                            Block.BLOCK_WIDTH,
                            Block.BLOCK_WIDTH),
                            _tileMap.getRectForTile(_tileIDBad + 1), tint);

                        sprites.Draw(this._tileMap.TileMapTexture,new Rectangle(
                            xPosition + Block.BLOCK_WIDTH,
                            yPosition,
                            Block.BLOCK_WIDTH,
                            Block.BLOCK_WIDTH),
                            _tileMap.getRectForTile(_tileIDBad + 2), tint);

                        sprites.Draw(this._tileMap.TileMapTexture, new Rectangle(
                           xPosition - Block.BLOCK_WIDTH,
                           yPosition,
                           Block.BLOCK_WIDTH,
                           Block.BLOCK_WIDTH),
                           _tileMap.getRectForTile(_tileIDBad), tint);


                        }



            }
            Engine.Alert = false;
            return sprites;
        }


    }
}
