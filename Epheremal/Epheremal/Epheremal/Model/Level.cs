﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Epheremal.Model.NonPlayables;
using Epheremal.Assets;
using System.Diagnostics;
using Epheremal.Model.Interactions;
using Epheremal.Model.Levels;
using Epheremal.Model.Behaviours;
using Epheremal.Model.Levels;

namespace Epheremal.Model
{
    class Level
    {
        private LinkedList<Block> _blocks;
        private LinkedList<Character> _characters;
        private LinkedList<Entity> _entities;
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
                block.RenderSelf(ref sprite);
            }
            foreach (Character character in _characters)
            {
                character.RenderSelf(ref sprite);
            }
            return sprite;

            throw new NotSupportedException();
        }

        public void movement()
        {

            double gravity = 0.015;

            foreach (Character c in _characters)
            {
                //Remove residual friction from acceleration while greater than nothing
                double resFriction = 0.25;
                if (c.XAcc > 0)
                {
                    c.XAcc -= resFriction * c.XAcc;
                    c.XVel += -0.01 * c.XVel;
                }
                else
                {
                    c.XAcc += resFriction * -1 * c.XAcc;
                    c.XVel -= 0.01 * c.XVel;
                }
                if (c.YAcc > 0)
                {
                    c.YAcc -= resFriction * c.YAcc;
                    c.YVel += -0.01 * c.YVel;
                }
                else
                {
                    c.YAcc += resFriction * -1 * c.YAcc;
                    c.YVel -= 0.01 * c.YVel;
                }

                //Add acceleration if less than terminal velocity as defined by vector product
                bool belowTerminal = Math.Sqrt(c.XVel * c.XVel + c.YVel * c.YVel) < Character.ABS_TERMINAL_VELOCITY;
                
                if (belowTerminal || (!belowTerminal && (c.XVel < 0 ^ c.XAcc < 0)))
                {
                    c.XVel += c.XAcc;
                }
                if (belowTerminal || (!belowTerminal && (c.YVel < 0 ^ c.YAcc < 0)))
                {
                    c.YVel += c.YAcc;
                }

                //Constant gravity
                c.YAcc += gravity; 

                c.PosX += c.XVel; c.PosY += c.YVel;
            }
        }

        public void interact()
        {
            //Detect collisions, and create appropriate interactions
            
            foreach (Character c in _characters)
            {
                foreach (Entity b in _entities)
                {
                    if (c == b) continue;
                    Rectangle cBounds = c.GetBoundingRectangle();
                    Rectangle bBounds = b.GetBoundingRectangle();
                    if (cBounds.Intersects(bBounds))
                    {
                        c.QueueInteractions(b.GetInteractionsFor(c));
                    }
                }
                c.PollInteractions();
            }

        }

        public void behaviour()
        {
            foreach (Character c in _characters)
            {
                c.DoBehaviour();
            }
        }

        public Boolean LoadLevel(Engine game, RawLevel rawLevel, TileMap tileMap)
        {
            _blocks = new LinkedList<Block>();
            _characters = new LinkedList<Character>();
            _entities = new LinkedList<Entity>();
            /*
            for (int i = 0; i < 10; i++)
            {
                Block _block = new Block(game, tileMap, rawLevel.State1[0]) { GridX = i, GridY = 15 };
                _block.Behaviours[EntityState.GOOD].Add(new Harmless());
                _blocks.AddFirst(_block);
                _entities.AddFirst(_block);
            }*/
            
            for (int y = 0; y < rawLevel.height; y++)
            {
                // Debug.WriteLine("");
                for (int x = 0; x < rawLevel.width; x++)
                {
                    // Debug.Write("|" + rawLevel.State1[y * rawLevel.width + x]);

                    int blockID = rawLevel.State1[y * rawLevel.width + x];

                    Block b = new Block(game, tileMap, blockID) { GridX = x, GridY = y };

                    switch (blockID){
                        case 1:
                            b.AssignBehaviour( new Dictionary<EntityState, List<Behaviour>> () {
                                {EntityState.GOOD, new List<Behaviour>()},
                                {EntityState.BAD, new List<Behaviour>()}
                        });
                            break;
                        default:
                            b.AssignBehaviour( new Dictionary<EntityState, List<Behaviour>> () {
                                {EntityState.GOOD, new List<Behaviour>(){new Harmless()}},
                                {EntityState.BAD, new List<Behaviour>(){new Harmless()}}
                        });
                            break;

                    }
                    
                    _blocks.AddLast(b);
                    _entities.AddLast(b);
                }
            }
            
            _characters.AddFirst(Engine.Player);
            _characters.AddFirst(new Goomba() { PosX = 100, PosY = 50, _texture = TextureProvider.GetBlockTextureFor(game, BlockType.TEST, EntityState.GOOD) });
            _characters.AddFirst(new Charger() { PosX = 100, PosY = 25, _texture = TextureProvider.GetBlockTextureFor(game, BlockType.TEST, EntityState.GOOD) });
            _characters.AddFirst(new Charger() { PosX = 150, PosY = 75, _texture = TextureProvider.GetBlockTextureFor(game, BlockType.TEST, EntityState.GOOD) });
            _characters.AddFirst(new Birdie(200, 350) { PosX = 250, PosY = 75, _texture = TextureProvider.GetBlockTextureFor(game, BlockType.TEST, EntityState.GOOD) });

            foreach (Character c in _characters) _entities.AddFirst(c);

            return true;
        }
    }
}
