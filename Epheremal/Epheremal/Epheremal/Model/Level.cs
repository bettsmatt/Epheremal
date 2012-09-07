using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Epheremal.Model
{
    class Level
    {
        private LinkedList<Block> _blocks;
        private LinkedList<Entity> _characters;
        private int _level;

        public Level(int level)
        {
        }
        /// <summary>
        /// The method responsible for populating the current sprite batch 
        /// with the content of the level, as visible from the viewport.
        /// </summary>
        /// <param name="sprite">Pass by reference sprite batch from the game engine</param>
        /// <returns>The spritebatch object. unnecessary as pass by ref, but good for testing</returns>
        public SpriteBatch RenderLevel(ref SpriteBatch sprite)
        {
            int x = 0;
            int y = 0;
            foreach (Block block in _blocks)
            {
                //accurately compute abs x and y from a grid position
                block.RenderSelf(ref sprite, x+=20, y+=20);
            }
            return sprite;
           
            throw new NotSupportedException();
        }

        public Boolean LoadLevel(Engine game)
        {
            _blocks = new LinkedList<Block>();
            for (int i = 0; i < 10; i++)
            {
                _blocks.AddLast(new Block(game));   
            }
            return true;
        }
    }
}
