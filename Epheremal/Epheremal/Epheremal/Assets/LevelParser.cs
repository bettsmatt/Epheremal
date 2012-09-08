using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Epheremal.Model.Levels;

namespace Epheremal.Assets
{
    class LevelParser
    {

        /*
         * Parse a tile map from a file , this is a sheet of sprites of the same width 
         * TILE MAPS MUST BE SQUARE!!!!! 
         */
        public static TileMap ParseTileMap(Engine game, string fileName, int tileSize) {

            ContentManager manager = new ContentManager(game.Services, "Content");
            Texture2D tileMap = manager.Load<Texture2D>(fileName);

            return new TileMap
            {
                Width = tileMap.Width,
                Height = tileMap.Height,
                TileMapTexture = tileMap,
                TileSize = tileSize
            };

        }

        /*
         * Parse a level from a tile, the level must have two states in the current implementation, line numbers are hard coded
         */ 
        public static RawLevel ParseTextFile(string filename) {

            try
            {
                using (StreamReader reader = new StreamReader(filename))
                {

                    string state1Line = null;
                    string state2Line = null;
                    string widthLine = null;
                    string heightLine = null;

                    int lineNum = 1;
                    while (!reader.EndOfStream)
                    {

                        /*
                         * Pull relevant lines from the file 
                         */
                        string line = reader.ReadLine();

                        if (lineNum == 4)
                            state1Line = line;

                        if (lineNum == 15)
                            state2Line = line;

                        if (lineNum == 5)
                            heightLine = line;

                        if (lineNum == 10)
                            widthLine = line;

                        lineNum++;
                    }

                    Debug.WriteLine("State1:" +state1Line);
                    Debug.WriteLine("State2:" + state2Line);
                    Debug.WriteLine("Width:" + widthLine);
                    Debug.WriteLine("Height:" + heightLine);

                    /*
                     * Extract values from lines
                     * */
                    string[] state1Data = state1Line.Split( new[] { '[', ']' })[1].Split(new[] { ',' });
                    string[] state2Data = state2Line.Split( new[] { '[', ']' })[1].Split(new[] { ',' });



                    int height = int.Parse(heightLine.Split(new[] { ':', ',' })[1]);
                    int width = int.Parse(widthLine.Split(new[] { ':',',' })[1]);

                    //foreach (string s in state1Data)
                        //Debug.WriteLine(s);

                    int[] state1Values = new int[width * height];
                    int[] state2Values = new int[width * height];

                    for( int i = 0 ; i < width * height ; i ++)
                    {
                        state1Values[i] = int.Parse(state1Data[i]);
                        state2Values[i] = int.Parse(state2Data[i]);
                    }

                    RawLevel rawlevel = new RawLevel();
                    rawlevel.height = height;
                    rawlevel.width = width;
                    rawlevel.State1 = state1Values;
                    rawlevel.State2 = state2Values;

                    return rawlevel;

                }
            }
            catch (Exception e) 
            {
                Debug.WriteLine("Could not read file:" +e);
            }

            return null;
        }
    }
}
