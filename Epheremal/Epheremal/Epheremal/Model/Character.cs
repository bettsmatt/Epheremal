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
        private int _posX { get; set; }
        private int _posY { get; set; }
    }
}
