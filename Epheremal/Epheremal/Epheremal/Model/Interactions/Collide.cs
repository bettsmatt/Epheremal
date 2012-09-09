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
            double dx = Interactor.GetBoundingRectangle().X - Interactee.GetBoundingRectangle().X;
            double dy = Interactor.GetY() - Interactee.GetY();
            double yVel = ((Character)Interactor).YVel;
            double xVel = ((Character)Interactor).XVel;

            double interactorAngle = Math.Atan2(yVel, xVel);
            double minimumReboundVelocity = 0.75;
            double friction = Engine.MarioControl ? 0.8 : 0.75;
            
            ///
            /// The collision mechanism will detect which side the collision has occurred on
            /// by inferring that the axis with least distance between centres of objects will
            /// therefore have been the first to collide. We then split each axis into the two
            /// cases of the cardinal direction of approach, again inferred from the difference
            /// in centres. Then, we apply a velocity and acceleration in the opposite direction
            /// of that of the collision. We ensure that multiple collisions occuring in the same
            /// interaction queue do not repeatedly flip the velocities and accelerations (i.e. by
            /// repearatedly multiplying by -1) by using a ternary to ensure that the resultant direction
            /// will always be opposite of the incoming collision direction. As the collisions are
            /// proportional to the speed of the colliding entities, we need to make sure we have a
            /// minimum possible ejection speed to avoid an entity appearing to be trapped in a collision
            /// with very low speed. 
            /// 
            /// Note also that in collisions between two characters, we apply the same collision to
            /// both interactor and interactee. The asture reader will note this will apply the collision
            /// ejection vectors twice to each character, which will nullify the apparent relative velocity
            /// being twice as high as that of a vs. static collision.
            ///

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
