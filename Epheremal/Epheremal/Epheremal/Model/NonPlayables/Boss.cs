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


    class Boss : NPC
    {
        int IDGood2, IDGood3, IDGood4, IDBad2, IDBad3, IDBad4;
        int[] goodTiles, badTiles;

        public Boss(TileMap tileMap, int tileIDGood, int tileIDGood2, int tileIDGood3, int tileIDGood4, int tileIDBad, int tileIDBad2, int tileIDBad3, int tileIDBad4)
            : base(tileMap, tileIDGood, tileIDBad)
        {
            IDGood2 = tileIDGood2;
            IDGood3 = tileIDGood3;
            IDGood4 = tileIDGood4;
            IDBad2 = tileIDBad2;
            IDBad3 = tileIDBad3;
            IDBad4 = tileIDBad4;
            this.Behaviours = new Dictionary<EntityState, List<Behaviour>>();

            this.Behaviours.Add(EntityState.GOOD,
                                new List<Behaviour>()
                                {
                                    new MoveToward(1, 500, true, true),
                                    new Deadly(),
                                    new Flies(false)
                                });
            this.Behaviours.Add(EntityState.BAD,
                                new List<Behaviour>()
                                {
                                    new Adhesive(),
                                    new MovePatrol(3,3,1)
                                });
            _width *= 4;
            _height *= 4;

            int[] gt = { _tileIDGood, IDGood2, IDGood3, IDGood4 };
            int[] bt = { _tileIDBad, IDBad2, IDBad3, IDBad4 };
            goodTiles = gt;
            badTiles = bt;
        }

        public override Rectangle GetBoundingRectangle()
        {
            int xPosition = Convert.ToInt32(PosX - Engine.xOffset), yPosition = Convert.ToInt32(PosY - Engine.yOffset);
            return new Rectangle(xPosition, yPosition, this._width, this._height);
        }

        public Rectangle GetSmallBoundingRectangle(int x, int y)
        {
            int xPosition = Convert.ToInt32(PosX - Engine.xOffset), yPosition = Convert.ToInt32(PosY - Engine.yOffset);
            return new Rectangle(xPosition + x * Block.BLOCK_WIDTH, yPosition + y * Block.BLOCK_WIDTH, Block.BLOCK_WIDTH, Block.BLOCK_WIDTH);
        }

        public override SpriteBatch RenderSelf(ref SpriteBatch sprites)
        {
            Color tint = Color.White;

            int[] tile = goodTiles;
            if (Entity.State == EntityState.BAD) tile = badTiles;

            int modifier = 0;
            if (XVel > 0) modifier = 4;


            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    sprites.Draw(this._tileMap.TileMapTexture, this.GetSmallBoundingRectangle(j, i), _tileMap.getRectForTile(tile[i] + (j + modifier)), tint);

                }
            }

            //sprites.Draw(this._tileMap.TileMapTexture, this.GetBoundingRectangle(), _tileMap.getRectForTile(_tileIDGood), tint);
                    
            /*

            int[] tile = { _tileIDGood, IDGood2, IDGood3, IDGood4 };

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    sprites.Draw(this._tileMap.TileMapTexture, this.GetSmallBoundingRectangle(j, i), _tileMap.getRectForTile(tile[i] + j), tint);

                }
            }*/
            return sprites;
        }
    }
}
