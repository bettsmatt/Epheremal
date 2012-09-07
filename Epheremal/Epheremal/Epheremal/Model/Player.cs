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
