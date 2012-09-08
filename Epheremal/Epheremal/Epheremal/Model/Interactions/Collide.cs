using System;
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
            double dx = Interactor.GetX() - Interactee.GetX();
            double dy = Interactor.GetY() - Interactee.GetY();
            double yVel = ((Character)Interactor).YVel;
            double xVel = ((Character)Interactor).XVel;

            double interactorAngle = Math.Atan2(yVel, xVel);


            if (Math.Abs(dx) > Math.Abs(dy))
            {
                Interactor.PosX -= xVel;
                //if (Interactor is Player || Interactee is Player) Debug.WriteLine("1--- dx: " + dx + " dy: " + dy);
                if (dx > 0)
                {
                    this.Interactor.XVel = -Interactor.XVel * 0.5;
                    this.Interactor.XAcc = -1.0 * this.Interactor.XAcc; //bounce a little 
                }
                else
                {
                    this.Interactor.XVel = -Interactor.XVel * 0.5;
                    this.Interactor.XAcc = -1.0 * this.Interactor.XAcc; //bounce a little 
                }
                
            }
            else
            {
                Interactor.PosY -= yVel;
                //if (Interactor is Player || Interactee is Player) Debug.WriteLine("2--- dx: " + dx + " dy: " + dy);
                if (dy > 0)
                {
                    this.Interactor.YVel = -Interactor.YVel * 0.5;
                    this.Interactor.YAcc = 1 * this.Interactor.YAcc * (this.Interactor.YAcc < 0 ? -1 : 1); //bounce a little
                }
                else
                {
                    this.Interactor.YVel = 0;
                    this.Interactor.YAcc = -0.3 * this.Interactor.YAcc; //bounce a little 
                }
                
            }
        }
    }
}
