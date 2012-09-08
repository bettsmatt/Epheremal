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

        bool fitstMove= true;

        public MovePatrol()
        {
            goingLeft = true;
            moveLeft = new MoveLeft();
            moveRight = new MoveRight();


        }

        public override void apply(Character character)
        {
            if (fitstMove) {
                left = (int) character.PosX - 20;
                right = (int) character.PosX + 20;
                fitstMove = false;
            }

            if (character.PosX < left) goingLeft = false;
            else if (character.PosX > right) goingLeft = true;

            if (goingLeft) moveLeft.apply(character);
            else moveRight.apply(character);
        }
    }
}
