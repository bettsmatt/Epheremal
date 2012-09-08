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

            if (Math.Abs(dx) > Math.Abs(dy))
            {
                //if (Interactor is Player || Interactee is Player) Debug.WriteLine("1--- dx: " + dx + " dy: " + dy);
                if (dx > 0)
                {
                    this.Interactor.XVel = 1;
                    this.Interactor.XAcc = -1.0 * this.Interactor.XAcc; //bounce a little 
                }
                else
                {
                    this.Interactor.XVel = -1;
                    this.Interactor.XAcc = -1.0 * this.Interactor.XAcc; //bounce a little 
                }
            }
            else
            {
                //if (Interactor is Player || Interactee is Player) Debug.WriteLine("2--- dx: " + dx + " dy: " + dy);
                if (dy > 0)
                {
                    this.Interactor.YVel = 1;
                    this.Interactor.YAcc = -0.3 * this.Interactor.YAcc; //bounce a little 
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
