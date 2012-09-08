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

        bool _bob;

        public Flies(bool bob) {
            _bob = bob;
        }

        int totalBob = 30;
        int currentBob = 15;
        string bobDirection = "up";

        public void apply(Character character)
        {
            //anti gravity
            character.YAcc -= Level.gravity;

            
            if(_bob){
                if (bobDirection == "up")
                {
                    currentBob++;
                    character.PosY -= 0.3;

                    if (currentBob > totalBob)
                    {
                        bobDirection = "down";
                        currentBob = 0;
                    }
                }

                else {

                    currentBob ++;
                    character.PosY += 0.3;
                    if (currentBob > totalBob)
                    {
                        bobDirection = "up";
                        currentBob = 0;
                    }
                }
            }
             

                
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
