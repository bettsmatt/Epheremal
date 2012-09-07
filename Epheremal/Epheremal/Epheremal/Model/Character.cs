using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Behaviours;

namespace Epheremal.Model
{
    abstract class Character : Entity
    {
        // Absolute values from the top left corner of the map
        protected int _posX { get; set; }
        protected int _posY { get; set; }

        public void DoBehaviour()
        {
            foreach (Behaviour behaviour in this.Behaviours[this.State])
            {
                behaviour.apply(this);
            }
        }
    }
}
