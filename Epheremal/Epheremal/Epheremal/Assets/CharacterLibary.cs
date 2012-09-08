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
            Behaviours.Add(getIDFor(0, 20), new Birdie(10, 30));
            Behaviours.Add(getIDFor(0, 19), new Charger());
            Behaviours.Add(getIDFor(0, 21), new Goomba());
            Behaviours.Add(getIDFor(0, 22), new Jumper());
            Behaviours.Add(getIDFor(0, 23), new Jumper());
            Behaviours.Add(getIDFor(0, 24), new Jumper());

           // Debug.WriteLine("Adding " + getIDFor(0, 19));
           // Debug.WriteLine("Adding " + getIDFor(0, 20));
          //  Debug.WriteLine("Adding " + getIDFor(0, 21));
          //  Debug.WriteLine("Adding " + getIDFor(0, 22));
          //  Debug.WriteLine("Adding " + getIDFor(0, 23));
          //  Debug.WriteLine("Adding " + getIDFor(0, 24));
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
