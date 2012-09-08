using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Behaviours;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Epheremal.Model.Interactions;
using Epheremal.Model.Levels;
using System.Diagnostics;
using Epheremal.Model.NonPlayables;

namespace Epheremal.Model
{
    public abstract class Character : Entity
    {
        public const double ABS_TERMINAL_VELOCITY_X = 5;
        public const double ABS_TERMINAL_VELOCITY_Y = 5;

        // Absolute values from the top left corner of the map
        public double PosX { get; set; }
        public double PosY { get; set; }

        public double XAcc { get; set; }
        public double YAcc { get; set; }

        public double XVel { get; set; }
        public double YVel { get; set; }

        public bool Jumping = false;
        public bool Animated = false;
        public bool Dead = false;

        public Character(TileMap tileMap, int tileIDGood, int tileIDBad) : base(tileMap, tileIDGood, tileIDBad) { }

        public void DoBehaviour()
        {
            if (Dead) return;
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
            foreach (Interaction i in toInteract)
                this.Interactions.Enqueue(i);
        }

        public override Rectangle GetBoundingRectangle()
        {
            int xPosition = Convert.ToInt32(PosX - Engine.xOffset), yPosition = Convert.ToInt32(PosY - Engine.yOffset);
            return new Rectangle(xPosition, yPosition, this._width, this._height);
        }

        public override SpriteBatch RenderSelf(ref SpriteBatch sprites)
        {
            if (Dead) return sprites;
            Color tint = Engine.Alert ? Color.Red : Color.White;
            if (Animated)
            {
                if (XVel < 0.2 && XVel > -0.2) sprites.Draw(this._tileMap.TileMapTexture, this.GetBoundingRectangle(), _tileMap.getRectForTile(_tileIDGood), tint);

                else if (XVel < 0)
                {
                    if (Jumping) sprites.Draw(this._tileMap.TileMapTexture, this.GetBoundingRectangle(), _tileMap.getRectForTile(_tileIDGood + AnimatedTexture.Frame + 1), tint);

                    else sprites.Draw(this._tileMap.TileMapTexture, this.GetBoundingRectangle(), _tileMap.getRectForTile(_tileIDGood + AnimatedTexture.Frame + 1), tint);
                }
                else
                {
                    if (Jumping) sprites.Draw(this._tileMap.TileMapTexture, this.GetBoundingRectangle(), _tileMap.getRectForTile(_tileIDGood + AnimatedTexture.Frame + 6), tint);

                    else sprites.Draw(this._tileMap.TileMapTexture, this.GetBoundingRectangle(), _tileMap.getRectForTile(_tileIDGood + AnimatedTexture.Frame + 6), tint);

                }
            }
            else
            {
                if (Entity.State == EntityState.GOOD)
                    if (XVel < 0)
                        sprites.Draw(this._tileMap.TileMapTexture, this.GetBoundingRectangle(), _tileMap.getRectForTile(_tileIDGood), tint);
                    else
                        sprites.Draw(this._tileMap.TileMapTexture, this.GetBoundingRectangle(), _tileMap.getRectForTile(_tileIDGood + 1), tint);

                else
                    if (XVel < 0)
                        sprites.Draw(this._tileMap.TileMapTexture, this.GetBoundingRectangle(), _tileMap.getRectForTile(_tileIDBad), tint);
                    else
                        sprites.Draw(this._tileMap.TileMapTexture, this.GetBoundingRectangle(), _tileMap.getRectForTile(_tileIDBad + 1), tint);
            }
            Engine.Alert = false;
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

        public void Kill()
        {
            // TODO PLEASE DO IT PROPERLY LOL
            PosX = -100;
            PosY = -100;
            Dead = true;
        }
    }
}
