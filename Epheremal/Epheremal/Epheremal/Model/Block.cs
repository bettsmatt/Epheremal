using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Assets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Epheremal.Model.Interactions;
using Epheremal.Model.Behaviours;
using Microsoft.Xna.Framework.Graphics;
using Epheremal.Model.Levels;
using System.Diagnostics;


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

        public const int BLOCK_WIDTH = 32; //magic!

        public Block(TileMap tileMap, int tileIDGood, int tileIDBad) : base(tileMap, tileIDGood, tileIDBad)
        {
        }

        public override Interaction[] GetInteractionsFor(Character interactor)
        {
            List<Interaction> retVal = new List<Interaction>();
            foreach (Behaviour b in this.Behaviours[Entity.State])
            {
                Interaction i = b.GetAppropriateInteractionFor(interactor,this);
                if (i != null) retVal.Add(i);
            }
            return retVal.ToArray<Interaction>();
        }

        public override Rectangle GetBoundingRectangle()
        {
            int xPosition = (GridX * _width) - Engine.xOffset, yPosition = (GridY * _height) - Engine.yOffset;
            return new Rectangle(xPosition, yPosition, this._width, this._height);
        }

        public override SpriteBatch RenderSelf(ref SpriteBatch sprites)
        {
            if(Entity.State == EntityState.GOOD)
                sprites.Draw(this._tileMap.TileMapTexture, this.GetBoundingRectangle(),_tileMap.getRectForTile(_tileIDGood), Color.White);
            else
                sprites.Draw(this._tileMap.TileMapTexture, this.GetBoundingRectangle(), _tileMap.getRectForTile(_tileIDBad), Color.White);

            return sprites;
        }

        public override double GetX()
        {
            return ((GridX * _width) + (_width / 2)) - Engine.xOffset;
        }

        public override double GetY()
        {
            return ((GridY * _height) + (_height / 2)) - Engine.yOffset;
        }
    }
}
