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
    enum EntityState
    {
        GOOD,
        BAD,
    }

    abstract class Entity
    {
        public Interaction Interaction {get; set;}
        public List<Behaviour> Behaviours {get; set;}

        private EntityState _state = EntityState.GOOD;
        public EntityState State { get { return _state; } set { _state = value; } }

        protected int _width = 20;
        protected int _height = 20;

        internal Texture2D _texture;
        
        public Rectangle GetBoundingRectangle(int x, int y)
        {
            return new Rectangle(x, y, this._width, this._height);
        }
    }
}
