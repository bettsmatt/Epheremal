using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Interactions;
using Epheremal.Model.Behaviours;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Epheremal.Model.Levels;
using Epheremal.Assets;

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

        protected Level _level;
        protected int _width = Block.BLOCK_WIDTH;
        protected int _height = Block.BLOCK_WIDTH;
        protected Rectangle _bounds;

        internal Texture2D _texture;

        public TileMap _tileMap;
        public int _tileIDGood;
        public int _tileIDBad;

        public Entity(TileMap tileMap, int tileIDGood, int tileIDBad)
        {
            this._tileIDGood = tileIDGood;
            this._tileIDBad = tileIDBad;
            this._tileMap = tileMap;
        }

        /*
         * Set the behaviours of an enity {GOOD, BAD}
         */
        public void AssignBehaviour(Dictionary<EntityState, List<Behaviour>> behaviours) {
            this.Behaviours = behaviours;
        }

        public abstract Interaction[] GetInteractionsFor(Character interactor);

        public abstract Rectangle GetBoundingRectangle();

        public abstract SpriteBatch RenderSelf(ref SpriteBatch sprites);

        public abstract double GetX();
        public abstract double GetY();

        public void SetLevel(Level level)
        {
            this._level = level;
        }
    }
}
