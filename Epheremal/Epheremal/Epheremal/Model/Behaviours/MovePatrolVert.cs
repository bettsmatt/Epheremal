using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epheremal.Model.Behaviours
{
    class MovePatrolVert : Move
    {
        Behaviour moveUp;
        Behaviour moveDown;
        int _upMax;
        int _downMax;
        int _upAmount;
        int _downAmount;
        bool goingUp;
        bool fitstMove= true;

        public MovePatrolVert ( int upAmount, float speedMod)
        {
            goingUp = true;
            moveUp = new MoveUp(speedMod);
            moveDown = new MoveDown(speedMod);

            _upAmount = upAmount;
            _downAmount = 0;

        }

        public override void apply(Character character)
        {
            if (fitstMove) {
                _upMax = (int)character.PosY - _upAmount;
                _downMax = (int)character.PosY + _downAmount;
                fitstMove = false;
            }

            if (character.PosY < _upMax) 
                goingUp = false;
            else if (character.PosY > _downMax) 
                goingUp = true;

            if (goingUp)
                character.YVel -= 0.1;
                //moveUp.apply(character);
            else
                character.YVel += 0.1;
                //moveDown.apply(character);
        }
    }
}
