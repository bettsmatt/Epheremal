using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Interactions;
using System.Diagnostics;

namespace Epheremal.Model.Behaviours
{
    class Flies : Behaviour
    {
        double bobEffect = 0.015;
        string bobDirection = "up";

        public void apply(Character character)
        {
            //anti gravity
            character.YAcc -= 0.015;

            if (bobEffect >= 0.015)
            {
                bobDirection = "down";
                }
            else if(bobEffect <= -0.015){
                bobDirection = "up";
            }

            character.YAcc -= bobEffect;

            if (bobDirection.Equals("up"))
            {
                
                bobEffect = bobEffect + 0.001;
            }
            else
            {
                
                bobEffect = bobEffect - 0.001;
            }

            //Debug.WriteLine("bobeffet: " + bobEffect + " bobDirection: " + bobDirection);

        }

        public Interaction GetAppropriateInteractionFor(Character interactor, Entity interactee)
        {
            return null;
        }
    }
}
