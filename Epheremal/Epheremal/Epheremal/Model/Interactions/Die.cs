using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epheremal.Model.Interactions
{
    class Die : InteractionBase
    {
        public Die(Character a, Entity b)
            : base(a, b)
        {

        }

        public override void Interact()
        {
            Interactor.KillFromCurrentLevel();
            SoundEffects.sounds["enemydeath"].Volume = 0.25f;
            SoundEffects.sounds["enemydeath"].Play();
        }
    }
}
