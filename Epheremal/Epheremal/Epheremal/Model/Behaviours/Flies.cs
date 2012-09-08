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
        double totalBob = 0.5;
        double currentBob = 0;
        string bobDirection = "up";

        public void apply(Character character)
        {
            //anti gravity
            character.YAcc -= Level.gravity;


            if (bobDirection == "up")
            {

                currentBob += 0.1;

                if (currentBob > totalBob)
                    bobDirection = "down";
            }

            else {

                currentBob -= 0.1;

                if (currentBob < -totalBob)
                    bobDirection = "up";
            }

            character.PosY += currentBob;
                
                /*
            if (bobEffect >= Level.gravity)
            {
                bobDirection = "down";
                }
            else if (bobEffect <= -Level.gravity)
            {
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
             */

            //Debug.WriteLine("bobeffet: " + bobEffect + " bobDirection: " + bobDirection);

        }

        public Interaction GetAppropriateInteractionFor(Character interactor, Entity interactee)
        {
            return null;
        }
    }
}
