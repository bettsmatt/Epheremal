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
        private Queue<Interaction> _interactions = new Queue<Interaction>();
        protected Queue<Interaction> Interactions { get { return _interactions; } set { _interactions = value; } }
        public Dictionary<EntityState, List<Behaviour>> Behaviours {get; set;}

        public static EntityState State = EntityState.GOOD;

        protected int _width = 20;
        protected int _height = 20;
        protected Rectangle _bounds;

        internal Texture2D _texture;

        public Entity()
        {
            this._bounds = Engine.Bounds;
        }

        public abstract Interaction GetInteractionFor(Entity interactor);

        public abstract Rectangle GetBoundingRectangle();

        public abstract SpriteBatch RenderSelf(ref SpriteBatch sprites);
    }
}
