using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Behaviours;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Epheremal.Model
{
    public abstract class Character : Entity
    {
        public const double ABS_TERMINAL_VELOCITY = 10;

        // Absolute values from the top left corner of the map
        public double PosX { get; set; }
        public double PosY { get; set; }

        public double XAcc {get; set;}
        public double YAcc {get; set;}

        public double XVel {get; set;}
        public double YVel {get; set;}

        public void DoBehaviour()
        {
            //null protection
            if (this.Behaviours == null) return;
            if (!this.Behaviours.ContainsKey(this.State)) return;
            if (this.Behaviours[this.State] == null) return;

            foreach (Behaviour behaviour in this.Behaviours[this.State])
            {
                behaviour.apply(this);
            }
        }

        public override SpriteBatch RenderSelf(ref SpriteBatch sprites, int offsetX, int offsetY)
        {
            if (_texture == null) return sprites;
            sprites.Draw(this._texture, this.GetBoundingRectangle(PosX + _width, PosY + _height), Color.White);
            return sprites;
        }
    }
}
