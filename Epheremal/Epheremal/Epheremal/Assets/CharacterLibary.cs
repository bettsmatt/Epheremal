using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Behaviours;
using Epheremal.Model.Levels;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Epheremal.Model;
using Epheremal.Model.NonPlayables;

namespace Epheremal.Assets
{
    /*
     * Provides a facility to look up behaviours for certian tiles
     */
    public class CharacterLibrary
    {

        private Dictionary<int, NPC> Behaviours = new Dictionary<int, NPC>();
        private TileMap tileMap;
        private int w;
        private int h;

        public CharacterLibrary(int width, int height)
        {

            w = width;
            h = height;

            /*
             * Some test characters 
             */
            Behaviours.Add(getIDFor(12, 16), new Birdie(tileMap, getIDFor(12, 16), getIDFor(12, 16), 10, 30));
            Behaviours.Add(getIDFor(13, 16), new Charger(tileMap, getIDFor(13, 16), getIDFor(13, 16)));
            Behaviours.Add(getIDFor(14, 16), new Goomba(tileMap, getIDFor(14, 16), getIDFor(14, 16)));
            Behaviours.Add(getIDFor(15, 16), new Jumper(tileMap, getIDFor(15, 16), getIDFor(15, 16)));
            Behaviours.Add(getIDFor(16, 16), new Birdie(tileMap, getIDFor(16, 16), getIDFor(16, 16), 20, 25));
        }

        public NPC get(int id)
        {
            //Debug.WriteLine("finding id " + id);
            if (Behaviours.ContainsKey(id))

                return Behaviours[id];

            else

                throw new Exception("ID - " + id + " Does not corropont to a character");

        }

        private int getIDFor(int x, int y)
        {

            int id = y * w + x + 1;

            return id;
        }


    }

}
