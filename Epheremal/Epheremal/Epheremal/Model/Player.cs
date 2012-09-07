using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Behaviours;
using Epheremal.Model.Interactions;

namespace Epheremal.Model
{
    public class Player : Character
    {
        Behaviour moveLeft = new MoveLeft();
        Behaviour moveRight = new MoveRight();
        Behaviour jump = new Jumps();
        List<Behaviour> currentBehaviours;

        public override Interactions.Interaction[] GetInteractionsFor(Character interactor)
        {
            List<Interaction> retVal = new List<Interaction>();
            foreach (Behaviour b in this.Behaviours[this.State])
            {
                Interaction i = b.GetAppropriateInteractionFor(interactor, this);
                if (i != null) retVal.Add(i);
            }
            return retVal.ToArray();
        }

        public Player()
        {
            Behaviours = new Dictionary<EntityState, List<Behaviour>>();
            currentBehaviours = new List<Behaviour>();
            Behaviours[EntityState.GOOD] = currentBehaviours;
            Behaviours[EntityState.BAD] = currentBehaviours;
        }

        public void movingLeft()
        {
            Behaviours[this.State].Clear();
            Behaviours[this.State].Add(moveLeft);
        }

        public void movingRight()
        {
            Behaviours[this.State].Clear();
            Behaviours[this.State].Add(moveRight);
        }

        public void notMoving()
        {
            Behaviours[this.State].Clear();
        }

        public void jumping()
        {
            Behaviours[this.State].Add(jump);
        }
    }
}
