using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epheremal.Model.Behaviours;
using Epheremal.Model.Levels;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Epheremal.Assets
{
    /*
     * Provides a facility to look up behaviours for certian tiles
     */ 
    public class TileLibrary
    {

        private Dictionary<int, List<Behaviour>> Behaviours = new Dictionary<int, List<Behaviour>>();
        private TileMap tileMap;

        public TileLibrary(TileMap t) {

            tileMap = t;

            /*
             * Build the dictionary 
             */

            List<Pair> land = new List<Pair>(){

                //Light Land
                new Pair{y = 0, x = 2},
                new Pair{y = 0, x = 3},
                new Pair{y = 0, x = 4},
                new Pair{y = 0, x = 5},
                new Pair{y = 0, x = 6},
                new Pair{y = 0, x = 7},

                new Pair{y = 1, x = 2},
                new Pair{y = 1, x = 3},
                new Pair{y = 1, x = 4},
                new Pair{y = 1, x = 5},
                new Pair{y = 1, x = 6},

                new Pair{y = 2, x = 2},
                new Pair{y = 2, x = 3},
                new Pair{y = 2, x = 4},
                new Pair{y = 2, x = 5},
                new Pair{y = 2, x = 6},

                new Pair{y = 3, x = 2},
                new Pair{y = 3, x = 3},
                new Pair{y = 3, x = 4},
                new Pair{y = 3, x = 5},

                // Dark Land
                new Pair{y = 4, x = 2},
                new Pair{y = 4, x = 3},
                new Pair{y = 4, x = 4},
                new Pair{y = 4, x = 5},
                new Pair{y = 4, x = 6},
                new Pair{y = 4, x = 7},

                new Pair{y = 5, x = 2},
                new Pair{y = 5, x = 3},
                new Pair{y = 5, x = 4},
                new Pair{y = 5, x = 5},
                new Pair{y = 5, x = 6},

                new Pair{y = 6, x = 2},
                new Pair{y = 6, x = 3},
                new Pair{y = 6, x = 4},
                new Pair{y = 6, x = 5},
                new Pair{y = 6, x = 6},

                new Pair{y = 7, x = 2},
                new Pair{y = 7, x = 3},
                new Pair{y = 7, x = 4},
                new Pair{y = 7, x = 5},

                // Bad Platform
                new Pair{y = 0, x = 12},
                new Pair{y = 0, x = 13},
                new Pair{y = 0, x = 14},

                // Rocks
                new Pair{y = 0, x = 19},
                new Pair{y = 0, x = 20},
                new Pair{y = 0, x = 21},
                new Pair{y = 0, x = 22},
                new Pair{y = 0, x = 23},
                new Pair{y = 0, x = 24},
                new Pair{y = 0, x = 25},

                new Pair{y = 1, x = 19},
                new Pair{y = 1, x = 20},
                new Pair{y = 1, x = 21},
                new Pair{y = 1, x = 22},
                new Pair{y = 1, x = 23},
                new Pair{y = 1, x = 24},
                new Pair{y = 1, x = 25},

                new Pair{y = 2, x = 19},
                new Pair{y = 2, x = 20},
                new Pair{y = 2, x = 21},
                new Pair{y = 2, x = 22},
                new Pair{y = 2, x = 23},
                new Pair{y = 2, x = 24},
                new Pair{y = 2, x = 25},


                // Green
                new Pair{y = 6, x = 19},
                new Pair{y = 6, x = 20},
                new Pair{y = 6, x = 21},
                new Pair{y = 6, x = 22},
                new Pair{y = 6, x = 23},
                new Pair{y = 6, x = 24},
                new Pair{y = 6, x = 25},

                new Pair{y = 7, x = 19},
                new Pair{y = 7, x = 20},
                new Pair{y = 7, x = 21},
                new Pair{y = 7, x = 22},
                new Pair{y = 7, x = 23},
                new Pair{y = 7, x = 24},
                new Pair{y = 7, x = 25},

                new Pair{y = 8, x = 19},
                new Pair{y = 8, x = 20},
                new Pair{y = 8, x = 21},
                new Pair{y = 8, x = 22},
                new Pair{y = 8, x = 23},
                new Pair{y = 8, x = 24},
                new Pair{y = 8, x = 25},

                new Pair{y = 9, x = 19},
                new Pair{y = 9, x = 20},
                new Pair{y = 9, x = 21}


            
            };

            foreach (Pair p in land)
            {
               //Debug.WriteLine("Adding " + p.x + " " + p.y + " " + getIDFor(p.x, p.y));
                Behaviours.Add(getIDFor(p.x, p.y), new List<Behaviour>() { new Harmless() });

            }

            // Sky
            Behaviours.Add(getIDFor(0, 0), new List<Behaviour>() {});

            // Brown Sky
            Behaviours.Add(getIDFor(0, 15), new List<Behaviour>() { });
        }

        public List<Behaviour> get(int id) {
            //Debug.WriteLine("finding id " + id);
            if(Behaviours.ContainsKey(id))

                return Behaviours[id];

            else 
   
               return new List<Behaviour>();
  
            }

        private int getIDFor(int x, int y) {

            int w = tileMap.Width / tileMap.TileSize;
            int h = tileMap.Height / tileMap.TileSize;

            int id = y * w + x + 1;

            return id;
        }


    }

    public class Pair {
        public int x;
        public int y;
    }          
}
