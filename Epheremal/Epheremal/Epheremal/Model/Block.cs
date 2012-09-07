using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Assets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Epheremal.Model
{
    enum BlockType
    {
        TEST,
    }


    class Block : Entity
    {
        public BlockType Type { get; set; }
        public Dictionary<Block, Block> Adjacencies { get; set; }

        public int GridX {get; set;}
        public int GridY {get; set;}

        public Block(Engine game)
        {
            this._texture = TextureProvider.GetBlockTextureFor(game, this.Type, this.State);
        }

        public SpriteBatch RenderSelf(ref SpriteBatch sprites, int offsetX, int offsetY)
        {
            int xPosition = (GridX * _width) - offsetX, yPosition = (GridY * _height) - offsetY;
            sprites.Draw(this._texture, this.GetBoundingRectangle(xPosition + _width, yPosition + _height), Color.White);
            return sprites;
        }
    }
}
