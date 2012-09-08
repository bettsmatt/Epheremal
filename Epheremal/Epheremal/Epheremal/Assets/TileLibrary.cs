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
        private int w;
        private int h;

        public TileLibrary(int width, int height) {

            w = width;
            h = height;

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
                new Pair{y = 3, x = 6},

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
                new Pair{y = 7, x = 6},

                // Bad Platform
                new Pair{y = 0, x = 12},
                new Pair{y = 0, x = 13},
                new Pair{y = 0, x = 14},

                new Pair{y = 2, x = 12},
                new Pair{y = 2, x = 13},
                new Pair{y = 2, x = 14},

                new Pair{y = 1, x = 16},
                new Pair{y = 1, x = 17},
                new Pair{y = 1, x = 18},

                new Pair{y = 2, x = 16},

                // Spike
                new Pair{x = 13, y = 4},
                new Pair{x = 14, y = 4},
                new Pair{x = 15, y = 4},
                new Pair{x = 16, y = 4},
                new Pair{x = 17, y = 4},

                new Pair{x = 14, y = 5},
                new Pair{x = 15, y = 5},
                new Pair{x = 16, y = 5},

                new Pair{x = 15, y = 6},
                new Pair{x = 15, y = 7},
                new Pair{x = 15, y = 8},
                new Pair{x = 15, y = 9},
                new Pair{x = 14, y = 10},
                new Pair{x = 15, y = 10},
                new Pair{x = 16, y = 10},

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

                new Pair{y = 3, x = 19},
                new Pair{y = 3, x = 20},
                new Pair{y = 3, x = 21},



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
                new Pair{y = 9, x = 21},
                new Pair{y = 9, x = 22},

                new Pair{y = 10, x = 19},
                new Pair{y = 10, x = 20},
                new Pair{y = 10, x = 21},
                new Pair{y = 10, x = 22},

                new Pair{y = 11, x = 19},
                new Pair{y = 11, x = 20},
                new Pair{y = 11, x = 21},
                new Pair{y = 11, x = 22},

                new Pair{y = 9, x = 25},
                new Pair{y = 9, x = 26},

            
            };



            foreach (Pair p in land)
            {
               //Debug.WriteLine("Adding " + p.x + " " + p.y + " " + getIDFor(p.x, p.y));
                Behaviours.Add(getIDFor(p.x, p.y), new List<Behaviour>() { new Harmless() });

            }

            List<Pair> deadly = new List<Pair>(){
                
                // Water
                new Pair {y = 11, x = 1 },
                new Pair {y = 11, x = 2 },
                new Pair {y = 12, x = 1 },
                new Pair {y = 12, x = 2 },
            
                // Spikes
                new Pair {y = 12, x = 14 },

                new Pair {y = 14, x = 14 },
                new Pair {y = 14, x = 13 },
                new Pair {y = 14, x = 12 },
                
                new Pair {y = 15, x = 14 },
                new Pair {y = 15, x = 13 },
                new Pair {y = 15, x = 12 },
                
                new Pair {y = 12, x = 15 },
                new Pair {y = 13, x = 15 },
                new Pair {y = 14, x = 15 },

                new Pair {y = 12, x = 16 },
                new Pair {y = 13, x = 16 },
                new Pair {y = 14, x = 16 },


            };

            foreach (Pair p in deadly ){
                Behaviours.Add(getIDFor(p.x, p.y), new List<Behaviour>() { new Deadly() });
            }

            // Sky
            Behaviours.Add(getIDFor(0, 0), new List<Behaviour>() {});

            // Brown Sky
            Behaviours.Add(getIDFor(0, 15), new List<Behaviour>() { });

            //Bounce pad
            Pair BouncePad = new Pair { y = 19, x = 4 };
            Behaviours.Add(getIDFor(4, 19), new List<Behaviour>() { new Harmless(), new Bouncy() });
            //Sticky/web
            Pair Web = new Pair { y = 19, x = 4 };
            Behaviours.Add(getIDFor(5, 19), new List<Behaviour>() { new Harmless(), new Adhesive() });

            Pair BoostLeft = new Pair { y = 20, x = 4 };
            Behaviours.Add(getIDFor(4, 20), new List<Behaviour>() { new Harmless(), new Boosts() });

            Pair BoostRight = new Pair { y = 20, x = 5 };
            Behaviours.Add(getIDFor(5, 20), new List<Behaviour>() { new Harmless(), new Boosts() });
        }

        public List<Behaviour> get(int id) {
            //Debug.WriteLine("finding id " + id);
            if(Behaviours.ContainsKey(id))

                return Behaviours[id];

            else 
   
               return new List<Behaviour>();
  
            }

        private int getIDFor(int x, int y) {

            int id = y * w + x + 1;

            return id;
        }


    }

    public class Pair {
        public int x;
        public int y;
    }          
}
