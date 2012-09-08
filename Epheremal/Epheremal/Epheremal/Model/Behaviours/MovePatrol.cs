using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epheremal.Model.Behaviours
{
    class MovePatrol : Move
    {
        Behaviour moveLeft;
        Behaviour moveRight;
        int left, right;
        int _leftAmount;
        int _rightAmount;
        bool goingLeft;
        bool fitstMove= true;

        public MovePatrol( int leftAmount, int rightAmount, float speedMod)
        {
            goingLeft = true;
            moveLeft = new MoveLeft(speedMod);
            moveRight = new MoveRight(speedMod);

            _leftAmount = leftAmount;
            _rightAmount = rightAmount;

        }

        public override void apply(Character character)
        {
            if (fitstMove) {
                left = (int) character.PosX - _leftAmount ;
                right = (int) character.PosX + _rightAmount;
                fitstMove = false;
            }

            if (character.PosX < left) goingLeft = false;
            else if (character.PosX > right) goingLeft = true;

            if (goingLeft) moveLeft.apply(character);
            else moveRight.apply(character);
        }
    }
}
