using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Behaviours;
using Epheremal.Model.Interactions;
using Epheremal.Model.Levels;

namespace Epheremal.Model
{
    public class Player : Character
    {
        Behaviour moveLeft = new MoveLeft();
        Behaviour moveRight = new MoveRight();
        Behaviour jump = new Jumps();
        List<Behaviour> currentBehaviours;

        public Boolean isDead = false;

        public override Interactions.Interaction[] GetInteractionsFor(Character interactor)
        {
            List<Interaction> retVal = new List<Interaction>();
            foreach (Behaviour b in this.Behaviours[Entity.State])
            {
                Interaction i = b.GetAppropriateInteractionFor(interactor, this);
                if (i != null) retVal.Add(i);
            }
            return retVal.ToArray();
        }

        public Player(TileMap tileMap, int tileIDGood, int tileIDBad) : base(tileMap, tileIDGood, tileIDBad)
        {
            Behaviours = new Dictionary<EntityState, List<Behaviour>>();
            currentBehaviours = new List<Behaviour>();
            Behaviours[EntityState.GOOD] = currentBehaviours;
            Behaviours[EntityState.BAD] = currentBehaviours;
        }


        public void movingLeft()
        {
            Behaviours[Entity.State].Clear();
            Behaviours[Entity.State].Add(moveLeft);
        }

        public void movingRight()
        {
            Behaviours[Entity.State].Clear();
            Behaviours[Entity.State].Add(moveRight);
        }

        public void notMoving()
        {
            Behaviours[Entity.State].Clear();
        }

        public void jumping()
        {
            Behaviours[Entity.State].Add(jump);
        }
    }
}
