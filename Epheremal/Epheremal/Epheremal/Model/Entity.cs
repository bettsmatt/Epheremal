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

        /*
         * Set the behaviours of an enity {GOOD, BAD}
         */
        public void AssignBehaviour(Dictionary<EntityState, List<Behaviour>> behaviours) {
            this.Behaviours = behaviours;
            /*
            Behaviours = new Dictionary<EntityState, List<Behaviour>>()
            {
                {EntityState.GOOD, new List<Behaviour>()},
                {EntityState.BAD, new List<Behaviour>()},
            };
             */
        }

        public abstract Interaction[] GetInteractionsFor(Character interactor);

        public abstract Rectangle GetBoundingRectangle();

        public abstract SpriteBatch RenderSelf(ref SpriteBatch sprites);

        public double GetX();
        public double GetY();
    }
}
