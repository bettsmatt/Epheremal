﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

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


            if (Math.Abs(dx) > Math.Abs(dy))
            {

                Interactor.YVel *= 0.98; //Friction

                if (dx > 0)
                {
                    Interactor.XVel *= -0.5;
                    this.Interactor.XAcc = -1.0 * this.Interactor.XAcc; //bounce a little 
                }
                else
                {
                    Interactor.XVel *= -0.5;
                    this.Interactor.XAcc = -1.0 * this.Interactor.XAcc; //bounce a little 
                }
                Interactor.PosX -= xVel;
            }
            else
            {

                Interactor.XVel *= 0.95; //Apply a small friction coefficient

                if (dy > 0)
                {
                    Interactor.YVel *= -0.5;
                    this.Interactor.YAcc = 1 * this.Interactor.YAcc * (this.Interactor.YAcc < 0 ? -1 : 1); //bounce a little
                }
                else
                {
                    Interactor.YVel = 0;
                    this.Interactor.YAcc = -0.3 * this.Interactor.YAcc; //bounce a little 
                }
                Interactor.PosY -= yVel;
            }
        }
    }
}
