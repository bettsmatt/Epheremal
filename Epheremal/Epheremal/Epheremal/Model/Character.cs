using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Behaviours;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Epheremal.Model.Interactions;
using Epheremal.Model.Levels;

namespace Epheremal.Model
{
    public abstract class Character : Entity
    {
        public const double ABS_TERMINAL_VELOCITY = 5;

        // Absolute values from the top left corner of the map
        public double PosX { get; set; }
        public double PosY { get; set; }

        public double XAcc {get; set;}
        public double YAcc {get; set;}

        public double XVel {get; set;}
        public double YVel {get; set;}

        public Boolean Jumping = false;

        public Character(TileMap tileMap, int tileIDGood, int tileIDBad) : base(tileMap, tileIDGood, tileIDBad)
        {
        }


        public void DoBehaviour()
        {

            //null protection
            if (this.Behaviours == null) return;
            if (this.Behaviours[Entity.State] == null) return;

            foreach (Behaviour behaviour in this.Behaviours[Entity.State])
            {
                behaviour.apply(this);
            }
        }

        public void PollInteractions()
        {
            while (Interactions.Count > 0)
                Interactions.Dequeue().Interact();
        }

        public void QueueInteractions(Interaction[] toInteract)
        {
            foreach(Interaction i in toInteract)
                this.Interactions.Enqueue(i);
        }

        public override Rectangle GetBoundingRectangle()
        {
            int xPosition = Convert.ToInt32(PosX - Engine.xOffset), yPosition = Convert.ToInt32(PosY - Engine.yOffset);
            return new Rectangle(xPosition, yPosition, this._width, this._height);
        }

        public override SpriteBatch RenderSelf(ref SpriteBatch sprites)
        {
            if (_texture == null) return sprites;
            sprites.Draw(this._texture, this.GetBoundingRectangle(), Color.White);
            return sprites;

            if (Entity.State == EntityState.GOOD)
                sprites.Draw(this._tileMap.TileMapTexture, this.GetBoundingRectangle(), _tileMap.getRectForTile(_tileIDGood), Color.White);

            else
                sprites.Draw(this._tileMap.TileMapTexture, this.GetBoundingRectangle(), _tileMap.getRectForTile(_tileIDBad), Color.White);

            return sprites;
        }

        public override double GetX()
        {
            return (PosX + (_width / 2)) - Engine.xOffset;
        }

        public override double GetY()
        {
            return (PosY + (_height / 2)) - Engine.yOffset;
        }
    }
}
