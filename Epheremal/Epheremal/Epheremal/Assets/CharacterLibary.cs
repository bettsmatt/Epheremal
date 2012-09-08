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

            /* Knight / Ghost
             * GOOD: A night that patrols +\-3 and kills
             * BAD: Turns into a harmless ghost that parrols +\-3
             */
            if (id == getIDFor(0, 19))
            {
                return new Knight_Ghost(tileMap, getIDFor(0, 19), getIDFor(2, 19));
            }

            /*
             * Fly / Wasp 
             * GOOD: Flys around between +2 and -2 from its spawn, harmless
             * BAD: Tracks and kills player
             */
            else if (id == getIDFor(0, 20))
            {
                return new Fly_Wasp(tileMap, getIDFor(0, 20), getIDFor(2, 20));
            }

            /*
             * Worm / Devil
             * GOOD: harmless worm, will move towards to player
             * BAD: devil will move towards player slowly and hurt
             */
            else if (id == getIDFor(0, 21))
            {
                return new Worm_Devil(tileMap, getIDFor(0, 21), getIDFor(2, 21));
            }

            /* Snail / Bull
             * GOOD: slow snail, will patrol +\- 3 will hurt
             * BAD: fast bull, tracks player and kills
             */
            else if (id == getIDFor(0, 22))
            {
                return new Snail_Bull(tileMap, getIDFor(0, 22), getIDFor(2, 22));
            }

            /* Bird / Block
             * GOOD: flys around patroling +/-3 , kills
             * BAD: turns into block 
             */
            else if (id == getIDFor(0, 23))
            {
                return new Bird_Block(tileMap, getIDFor(0, 23), getIDFor(2, 23));
            }

            /* Shrom_Man
             * GOOD: A mushroom, harmless
             * BAD: A Mushrrom and that kills you and patrols +\- 3
             */
            else if (id == getIDFor(0, 24))
            {
                return new Shrom_Man(tileMap, getIDFor(0, 24), getIDFor(2, 24));
            }

            else if (id == getIDFor(0, 25))
            {
                return new Coin(tileMap, getIDFor(0, 25), getIDFor(0, 25));
            }
            

            else

                throw new Exception("ID - " + id + " Does not correspond to a character");

        }

        private int getIDFor(int x, int y)
        {

            int id = y * w + x + 1;

            return id;
        }


    }

}
