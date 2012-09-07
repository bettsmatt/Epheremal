using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Assets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Epheremal.Model.Levels;

namespace Epheremal.Model
{
    public enum BlockType
    {
        TEST,
    }


    public class Block : Entity
    {
        public BlockType Type { get; set; }
        public Dictionary<Block, Block> Adjacencies { get; set; }

        public int GridX {get; set;}
        public int GridY {get; set;}

        public TileMap _tileMap;
        public int _tileID;

        public Block(Engine game, TileMap tileMap, int tileID)
        {
            this._tileID = tileID;
            this._tileMap = tileMap;
            this._texture = TextureProvider.GetBlockTextureFor(game, this.Type, Entity.State);
        }

        public override Interactions.Interaction GetInteractionFor(Entity interactor)
        {
            throw new NotImplementedException();
        }

        public override Rectangle GetBoundingRectangle()
        {
            int xPosition = (GridX * _width) - Engine.xOffset, yPosition = (GridY * _height) - Engine.yOffset;
            return new Rectangle(xPosition, yPosition, this._width, this._height);
        }

        public override SpriteBatch RenderSelf(ref SpriteBatch sprites)
        {
            sprites.Draw(this._tileMap.TileMapTexture, this.GetBoundingRectangle(),_tileMap.getRectForTile(_tileID), Color.White);
            return sprites;
        }
    }
}
