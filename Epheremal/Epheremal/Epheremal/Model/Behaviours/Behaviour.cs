using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epheremal.Model.Behaviours
{
    interface Behaviour
    {
        void apply(Character toChar);
    }
}
