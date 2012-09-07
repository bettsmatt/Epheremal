using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Epheremal.Model.NonPlayables;
using Epheremal.Assets;

namespace Epheremal.Model
{
    class Level
    {
        private LinkedList<Block> _blocks;
        private LinkedList<Character> _characters;
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
            foreach (Block block in _blocks)
            {
                //accurately compute abs x and y from a grid position
                block.RenderSelf(ref sprite, 0, 0);
            }
            foreach (Character character in _characters)
            {
                character.RenderSelf(ref sprite, 0, 0);
            }
            return sprite;
           
            throw new NotSupportedException();
        }

        public void movement()
        {
            foreach (Character c in _characters)
            {
                c.XVel += c.XAcc; c.YVel += c.YAcc;
                c.PosX += c.XVel; c.PosY += c.YVel;
            }
        }

        public void interact()
        {

        }

        public void behaviour()
        {
            foreach (Character c in _characters)
            {
                c.DoBehaviour();
            }
        }

        public Boolean LoadLevel(Engine game)
        {
            _blocks = new LinkedList<Block>();
            _characters = new LinkedList<Character>();
            int closure = 0;
            for (int i = 0; i < 10; i++)
            {
                _blocks.AddLast(new Block(game) { GridX = closure+i, GridY = closure+i });   
            }
            _characters.AddFirst(Engine.Player);
            _characters.AddFirst(new Goomba() { PosX = 100, PosY = 50, _texture = TextureProvider.GetBlockTextureFor(game, BlockType.TEST, EntityState.GOOD)});
            
            return true;
        }
    }
}
