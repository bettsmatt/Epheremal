﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Interactions;
using Epheremal.Model.Behaviours;
using Epheremal.Model.Levels;

namespace Epheremal.Model
{
    public class NPC : Character
    {
        public NPC(TileMap tileMap, int tileIDGood, int tileIDBad) : base(tileMap, tileIDGood, tileIDBad)
        {
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
    }
}
