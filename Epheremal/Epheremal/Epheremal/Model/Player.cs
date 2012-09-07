using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Behaviours;

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
            throw new NotImplementedException();
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
