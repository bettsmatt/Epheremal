﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Assets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Epheremal.Model.Interactions;
using Epheremal.Model.Behaviours;

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

        public Block(Engine game) : base()
        {
            this._texture = TextureProvider.GetBlockTextureFor(game, this.Type, this.State);
        }

        public override Interaction[] GetInteractionsFor(Character interactor)
        {
            List<Interaction> retVal = new List<Interaction>();
            foreach (Behaviour b in this.Behaviours[this.State])
            {
                retVal.Add(b.GetAppropriateInteractionFor(interactor,this));
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
            sprites.Draw(this._texture, this.GetBoundingRectangle(), Color.White);
            return sprites;
        }
    }
}
