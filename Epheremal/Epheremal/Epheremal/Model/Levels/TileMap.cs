using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Epheremal.Model.Levels
{
    public class TileMap
    {
        public int Width;
        public int Height;
        public Texture2D TileMapTexture;
        public int TileSize;

        public Rectangle getRectForTile(int id)
        {

            id -= 1;
            int w = Width / TileSize ;
            int h = Height / TileSize ;

            //Debug.WriteLine(Width + "-" + Height);

            int y = id / h;
            int x = id % w;

            //Debug.WriteLine("TileID:"+id +" is been given X:"+x +", Y:" +y+ ", W:"+w +",H:"+h );
            return new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize);
        }
    
    }




}
