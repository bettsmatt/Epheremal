using System;
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

namespace Epheremal.Model
{
    public class Level
    {
        public const double gravity = 0.0981;
        private LinkedList<Block> _blocks;
        private LinkedList<Character> _characters;
        private LinkedList<Entity> _entities;
        private Queue<Character> _toKill = new Queue<Character>();
        private RawLevel _raw;

        private int _level;
        private int _levelScore = 0;

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
        /// 
        /// Manage the level specific score
        /// 
        public void AwardScore()
        {
            Engine.Player.score += this._levelScore;
        }

        public void AddLevelScore(int credit)
        {
            if(credit >= 0)
                this._levelScore += credit;
        }

        public void ClearLevelScore()
        {
            this._levelScore = 0;
        }

        public int GetScore() { return this._levelScore; }
        /// Endregion scores
        /// 
        

        public void movement()
        {
            foreach (Character c in _characters)
            {


                // Skip inamiate
                foreach (Behaviour b in c.Behaviours[Entity.State])
                    if(b is Inanimate)
                        continue;

                //Remove residual friction from acceleration while greater than nothing
                double resFriction = 0.1;
                if (c.XAcc > 0)
                {
                    c.XAcc -= resFriction * c.XAcc;
                    c.XVel += -0.01 * c.XVel;
                }
                else
                {
                    c.XAcc -= resFriction * c.XAcc;
                    c.XVel -= 0.01 * c.XVel;
                }
                if (c.YAcc > 0)
                {
                    c.YAcc -= resFriction * c.YAcc;
                    c.YVel += -0.01 * c.YVel;
                }
                else
                {
                    c.YAcc -= resFriction * c.YAcc;
                    c.YVel -= 0.01 * c.YVel;
                }

                bool belowTerminalX = c.XVel < Character.ABS_TERMINAL_VELOCITY_X && c.XVel > -Character.ABS_TERMINAL_VELOCITY_X;
                bool belowTerminalY = c.YVel < Character.ABS_TERMINAL_VELOCITY_Y && c.YVel > -Character.ABS_TERMINAL_VELOCITY_Y;

                /*if (belowTerminalX)
                    c.XVel += c.XAcc;


                if(belowTerminalY)
                    c.YVel += c.YAcc;
                */
                
                if (belowTerminalX || (!belowTerminalX && (c.XVel < 0 ^ c.XAcc < 0)))
                {
                    c.XVel += c.XAcc;
                }
                if (belowTerminalY || (!belowTerminalY && (c.YVel < 0 ^ c.YAcc < 0)))
                {
                    c.YVel += c.YAcc;
                }
                
                //Constant gravity
                c.YAcc += gravity; 

                c.PosX += c.XVel; c.PosY += c.YVel;

                if (c.PosX < 0) c.PosX = 0;
                if (c.PosX+c.GetBoundingRectangle().Width > GetLevelWidthInPixels())
                    c.PosX = GetLevelWidthInPixels()-c.GetBoundingRectangle().Width;
                // If it is too slow set to 0

                //if (c.YVel < 0.01 && c.YVel > -0.01) c.YVel = 0;
                //if (c.XVel < 0.01 && c.XVel > -0.01) c.XVel = 0;
                //if (c.YAcc < 0.01 && c.YAcc > -0.01) c.YAcc = 0;
                //if (c.XAcc < 0.01 && c.XAcc > -0.01) c.XAcc = 0;

                //Those characters that drop below the bottom of the screen, regardless of lethal components
                //should still be removed, at least to improve on computation costs.
                if (c.GetY() > _raw.height*Block.BLOCK_WIDTH)
                {                    
                    _toKill.Enqueue(c);
                }
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
            while (_toKill.Count > 0)
            {
                Character c = _toKill.Dequeue();
                if (c is Player)
                {
                    ((Player)c).isDead = true; ((Player)c).lives++;
                }
                else
                {
                    _entities.Remove(c);
                    _characters.Remove(c);
                }
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

                    if (!(b.Behaviours[EntityState.GOOD].Count == 0 && b.Behaviours[EntityState.BAD].Count == 0))
  
                        _entities.AddLast(b);



                    /*
                     * Check for characters
                     */ 
                    int characterId = rawLevel.Characters[y * rawLevel.width + x];
                    if(characterId != 0){
                        Character c = characterLibrary.get(characterId);
                        c.PosX = x * Block.BLOCK_WIDTH;
                        c.PosY = y * Block.BLOCK_WIDTH;

                        _characters.AddFirst(c);
                        _entities.AddFirst(c);
                    }

                 }
            }
            
            _characters.AddFirst(Engine.Player);
            _entities.AddFirst(Engine.Player);
            foreach (Entity e in _entities) e.SetLevel(this);

            return true;
        }

        public bool ValidateToggle()
        {
            EntityState state = Entity.State == EntityState.GOOD ? EntityState.BAD : EntityState.GOOD;
            foreach (Block b in _blocks)
            {
                if (!b.Behaviours[state].Exists(e => e is Harmless)) continue;
                if (b.GetBoundingRectangle().Intersects(Engine.Player.GetBoundingRectangle())) return false;
            }
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

        public void Kill(Character c)
        {
            _toKill.Enqueue(c);
        }
    }
}
