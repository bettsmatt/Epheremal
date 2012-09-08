using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework.Audio;

namespace Epheremal.Model.Interactions
{
    class Collide : InteractionBase
    {
        public Collide(Character a, Entity b)
            : base(a, b)
        {

        }

        public override void Interact()
        {
            applyTo(Interactor, Interactee);
            if (Interactee is Character) applyTo((Character)Interactee, Interactor);
        }

        private void applyTo(Character Interactor, Entity Interactee)
        {
            double dx = Interactor.GetX() - Interactee.GetX();
            double dy = Interactor.GetY() - Interactee.GetY();
            double yVel = ((Character)Interactor).YVel;
            double xVel = ((Character)Interactor).XVel;

            double interactorAngle = Math.Atan2(yVel, xVel);
            double minimumReboundVelocity = 0.75;
            double friction = Engine.MarioControl ? 0.8 : 0.75;

            if (Math.Abs(dx) > Math.Abs(dy))
            {

                Interactor.YVel *= friction; //Friction
                
                if (dx > 0)
                {
                    Interactor.PosX -= Math.Min(xVel, -minimumReboundVelocity);
                    Interactor.XVel *= 0.3 * (Interactor.XAcc < 0 ? -1 : 1);
                    Interactor.XAcc *= 0.3 * (this.Interactor.XAcc < 0 ? -1 : 1); //bounce a little 
                }
                else
                {
                    Interactor.PosX -= Math.Max(xVel, minimumReboundVelocity);
                    Interactor.XVel *= 0.5 * (Interactor.XAcc > 0 ? -1 : 1);
                    Interactor.XAcc *= 0.3 * (this.Interactor.XAcc > 0 ? -1 : 1); ; //bounce a little 
                }
                if (SoundEffects.sounds["hurt"].State == SoundState.Stopped && Interactor is Player && Interactor.XVel >0.5)
                {
                    SoundEffects.sounds["hurt"].Volume = 0.75f;
                    // soundInstance.IsLooped = False;
                    SoundEffects.sounds["hurt"].Play();
                }
            }
            else
            {

                Interactor.XVel *= friction; //Apply a small friction coefficient
                
                if (dy > 0)
                {
                    Interactor.PosY -= Math.Min(yVel, -minimumReboundVelocity);
                    Interactor.YVel *= 0.5 * (Interactor.YVel > 0 ? -1 : 1);
                    Interactor.YAcc = 0.3 * Interactor.YAcc * (Interactor.YAcc < 0 ? -1 : 1); //bounce a little
                }
                else
                {
                    Interactor.PosY -= yVel;
                    Interactor.YVel = 0;
                    Interactor.YAcc = 0.3 * Interactor.YAcc * (Interactor.YAcc > 0 ? -1 : 1); //bounce a little 
                    Interactor.Jumping = false;
                }
                
                
            }

        }
    }
}
