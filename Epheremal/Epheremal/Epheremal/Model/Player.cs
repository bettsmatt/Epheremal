using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Behaviours;
using Epheremal.Model.Interactions;
using Epheremal.Model.Levels;
using System.Diagnostics;

namespace Epheremal.Model
{
    public class Player : Character
    {
        Behaviour moveLeft = new MoveLeft(1);
        Behaviour moveRight = new MoveRight(1);
        Behaviour jump = new Jumps();
        List<Behaviour> currentBehaviours;


        int oldDirection = 0; // 1 for right, 0 for still, -1 for left

        public int score;
        public int lives = 3;


        public Boolean isDead = false;

        public Player(TileMap tileMap, int tileIDGood, int tileIDBad)
            : base(tileMap, tileIDGood, tileIDBad)
        {
            Animated = true;
            Behaviours = new Dictionary<EntityState, List<Behaviour>>();
            currentBehaviours = new List<Behaviour>();
            Behaviours[EntityState.GOOD] = currentBehaviours;
            Behaviours[EntityState.BAD] = currentBehaviours;
        }


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

        public void movingLeft()
        {
            if (Engine.MarioControl)
            {
                if (oldDirection != -1)
                {
                    //XVel = 0;
                    //XAcc = 0;
                }
                oldDirection = -1;
            }
            Behaviours[Entity.State].Clear();
            Behaviours[Entity.State].Add(moveLeft);
        }

        public void movingRight()
        {
            if (Engine.MarioControl)
            {
                if (oldDirection != 1)
                {
                    XVel = 0;
                    XAcc = 0;
                }
                oldDirection = 1;
            }
            Behaviours[Entity.State].Clear();
            Behaviours[Entity.State].Add(moveRight);
        }

        public void notMoving()
        {

            if (Engine.MarioControl)
            {
                oldDirection = 0;
                if (XVel > 0)
                {
                    XVel--;
                    if (XVel < 0)
                       XVel = 0;
                }

                if (XVel < 0)
                {
                   XVel++;
                   if (XVel > 0) ;
                    XVel = 0;
                }

                XAcc = 0;
            }
            Behaviours[Entity.State].Clear();
        }

        public void jumping()
        {
            Behaviours[Entity.State].Add(jump);
        }
    }
}
