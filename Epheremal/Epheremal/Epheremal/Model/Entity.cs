using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Interactions;
using Epheremal.Model.Behaviours;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Epheremal.Model
{
    public enum EntityState
    {
        GOOD,
        BAD,
    }

    public abstract class Entity
    {
        public Dictionary<EntityState, List<Interaction>> Interactions {get; set;}
        public Dictionary<EntityState, List<Behaviour>> Behaviours {get; set;}

        private EntityState _state = EntityState.GOOD;
        public EntityState State { get { return _state; } set { _state = value; } }

        protected int _width = 20;
        protected int _height = 20;
        protected Rectangle _bounds;

        internal Texture2D _texture;

        public Entity()
        {
            this._bounds = Engine.Bounds;
        }

        public Rectangle GetBoundingRectangle(double x, double y)
        {
            return new Rectangle(Convert.ToInt32(x), Convert.ToInt32(y), this._width, this._height);
        }

        public abstract SpriteBatch RenderSelf(ref SpriteBatch sprites, int offsetX, int offsetY);
    }
}
