using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epheremal.Model.Behaviours
{
    class MovePatrol : Move
    {
        Behaviour moveLeft = new MoveLeft();
        Behaviour moveRight = new MoveRight();
        int left, right;
        bool goingLeft;

        public MovePatrol(int l, int r)
        {
            goingLeft = true;
            moveLeft = new MoveLeft();
            moveRight = new MoveRight();

            left = l;
            right = r;
        }

        public override void apply(Character character)
        {
            if (character.PosX < left) goingLeft = false;
            else if (character.PosX > right) goingLeft = true;

            if (goingLeft) moveLeft.apply(character);
            else moveRight.apply(character);
        }
    }
}
