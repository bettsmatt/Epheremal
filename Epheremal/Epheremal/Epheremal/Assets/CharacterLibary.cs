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

        private Dictionary<int, Character> Behaviours = new Dictionary<int, Character>();
        private int w;
        private int h;
        TileMap tileMap;

        public CharacterLibrary(TileMap t, int width, int height)
        {

            w = width;
            h = height;
            tileMap = t;
            /*
             * Some test characters 
             */

        }

        public Character get(int id)
        {

            // Knight / Ghostr
            if (id == getIDFor(0, 19)) {
                return new Birdie(tileMap, getIDFor(0, 19), getIDFor(2, 19));
            }

            // Fly / Wasp
            else if (id == getIDFor(0, 20))
            {
                return new Jumper(tileMap, getIDFor(0, 20), getIDFor(2, 20));
            }
            
            // Worm / Devil
            else if (id == getIDFor(0, 21))
            {
                return new Goomba(tileMap, getIDFor(0, 21), getIDFor(2, 21));
            }
            
            // Snail / Bull
            else if (id == getIDFor(0, 22))
            {
                return new Charger(tileMap, getIDFor(0, 22), getIDFor(2, 22));
            }

            //Bird /Block
            else if (id == getIDFor(0, 23))
            {
                return new Birdie(tileMap, getIDFor(0, 23), getIDFor(2, 23));
            }

            // Mrushroom /Man
            else if (id == getIDFor(0, 24))
            {
                return new Birdie(tileMap, getIDFor(0, 24), getIDFor(2, 24));
            }

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
