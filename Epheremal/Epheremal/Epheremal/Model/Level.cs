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
        public const double gravity = 0.3;
        private LinkedList<Block> _blocks;
        private LinkedList<Character> _characters;
        private LinkedList<Entity> _entities;
        private RawLevel _raw;
        private int _level;

        public Level(int level)
        {
            this._level = level;
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
                double absX = block.GridX * Block.BLOCK_WIDTH, absY = block.GetY();
                //only render those blocks which are within the screen
                if(absX > (Engine.xOffset-2*Block.BLOCK_WIDTH) && absY < (Engine.xOffset+Engine.Bounds.Width+Block.BLOCK_WIDTH))
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


            foreach (Character c in _characters)
            {
                //Remove residual friction from acceleration while greater than nothing
                double resFriction = 0.3;
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

                if (c.PosX < 0) c.PosX = 0;
                if (c.PosX+c.GetBoundingRectangle().Width > GetLevelWidthInPixels()) c.PosX = GetLevelWidthInPixels()-c.GetBoundingRectangle().Width;
                // If it is too slow set to 0

                //if (c.YVel < 0.01 && c.YVel > -0.01) c.YVel = 0;
                //if (c.XVel < 0.01 && c.XVel > -0.01) c.XVel = 0;
                //if (c.YAcc < 0.01 && c.YAcc > -0.01) c.YAcc = 0;
                //if (c.XAcc < 0.01 && c.XAcc > -0.01) c.XAcc = 0;

            }
        }

        public void interact()
        {
            //Detect collisions, and create appropriate interactions
            
            foreach (Character c in _characters)
            {
                foreach (Entity b in _entities)
                {
                    if (c == b) continue;//cannot interact with self
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
            _raw = rawLevel;

            CharacterLibrary characterLibrary = new CharacterLibrary(
                tileMap,
                tileMap.Width / tileMap.TileSize,
                tileMap.Height / tileMap.TileSize
            );   

            TileLibrary tileLibrary = new TileLibrary(
                tileMap.Width / tileMap.TileSize ,
                tileMap.Height / tileMap.TileSize
            );

            for (int y = 0; y < rawLevel.height; y++)
            {
                for (int x = 0; x < rawLevel.width; x++)
                {

                    /*
                     * Check for blocks
                     */ 

                    int blockIDGood = rawLevel.State1[y * rawLevel.width + x];
                    int blockIDBad = rawLevel.State2[y * rawLevel.width + x];

                    Block b = new Block(tileMap, blockIDGood, blockIDBad) { GridX = x, GridY = y };

                    b.AssignBehaviour(
                        new Dictionary<EntityState, List<Behaviour>>() {
                                {EntityState.GOOD, tileLibrary.get(blockIDGood)},
                                {EntityState.BAD, tileLibrary.get(blockIDBad)}
                        });

                    _blocks.AddLast(b);
                    _entities.AddLast(b);

                    /*
                     * Check for characters
                     */ 
                    int characterId = rawLevel.Characters[y * rawLevel.width + x];
                    if(characterId != 0){
                        Character c = characterLibrary.get(characterId);
                        c.PosX = x * 10;
                        c.PosY = y * 10;

                        _characters.AddFirst(c);
                        _entities.AddFirst(c);
                    }

                 }
            }

            _characters.AddFirst(Engine.Player);
            _entities.AddFirst(Engine.Player);

            return true;
        }

        public Double GetLevelWidthInPixels()
        {
            if (_raw == null) return 0;
            return _raw.width * Block.BLOCK_WIDTH;
        }
        public Double GetLevelHeightInPixels()
        {
            if (_raw == null) return 0;
            return _raw.height * Block.BLOCK_WIDTH;
        }
    }
}
