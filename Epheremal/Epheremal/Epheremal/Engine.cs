using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Epheremal.Model;
using Epheremal.Assets;
using Epheremal.Model.Levels;

using System.Diagnostics;

namespace Epheremal
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Engine : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Rectangle Bounds;
        public static Player Player;
        public static int xOffset {get; set;}
        public static int yOffset {get; set;}

        private Level _currentLevel;
        private bool _toggleKeyPressed;
        private bool _toggleButtonPressed;

        public Engine()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Bounds = GraphicsDevice.Viewport.Bounds;
            Player = new Player()
            {
                _texture = TextureProvider.GetBlockTextureFor(this, BlockType.TEST, EntityState.GOOD),
            };
            //LevelParser.ParseTextFile("test.level");

            _currentLevel = new Level(1);

            TileMap tileMap = LevelParser.ParseTileMap(this, "tilemap", 32);
            RawLevel rawLevel = LevelParser.ParseTextFile("../../../../EpheremalContent/test.level");


            _currentLevel.LoadLevel(this, rawLevel, tileMap);


            _currentLevel.LoadLevel(this,rawLevel,tileMap);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            SoundEffects.sounds.Add("jump", Content.Load<SoundEffect>("jump").CreateInstance());
            SoundEffects.sounds.Add("hurt", Content.Load<SoundEffect>("hurt").CreateInstance());
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            getInput();
            //Scroll the viewport left and right when the player moves into the quarter of the screen on either side of the viewport,
            //we stop scrolling when the offset is flush to the left hand side (0), or right hand side (total width minus viewport width)
            if ((Player.PosX - Engine.xOffset) > (3*Bounds.Width / 4) && (Engine.xOffset < (_currentLevel.GetLevelWidthInPixels()-Bounds.Width)) && Player.XVel > 0) 
                    xOffset += Convert.ToInt32(Player.XVel);
            if ((Player.PosX - Engine.xOffset) < (Bounds.Width / 4) && Engine.xOffset > 0 && Player.XVel < 0)
                    xOffset += Convert.ToInt32(Player.XVel);
            _currentLevel.movement();
            _currentLevel.interact();
            _currentLevel.behaviour();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //TEST THINGS
            spriteBatch.Begin();
            spriteBatch = _currentLevel.RenderLevel(ref spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }


        private void getInput()
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboardState = Keyboard.GetState();

            // Move left
            if (gamePadState.DPad.Left == ButtonState.Pressed || gamePadState.ThumbSticks.Left.X < 0 || keyboardState.IsKeyDown(Keys.Left))
            {
                Player.movingLeft();
            }
            // Move right
            else if (gamePadState.DPad.Right == ButtonState.Pressed || gamePadState.ThumbSticks.Left.X > 0 || keyboardState.IsKeyDown(Keys.Right))
            {
                Player.movingRight();
            }
            else
            {
                Player.notMoving();
            }
            // Jump
            if (gamePadState.DPad.Up == ButtonState.Pressed || gamePadState.Buttons.A == ButtonState.Pressed || gamePadState.ThumbSticks.Left.Y > 0 || keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.Space))
            {
                Player.jumping();
            }

            // Change world state
            if ((gamePadState.Buttons.B == ButtonState.Released && _toggleButtonPressed) || (keyboardState.IsKeyUp(Keys.LeftShift) && _toggleKeyPressed))
            {
                if (Entity.State == EntityState.GOOD) Entity.State = EntityState.BAD;
                else Entity.State = EntityState.GOOD;
            }
            _toggleKeyPressed = keyboardState.IsKeyDown(Keys.LeftShift);
            _toggleButtonPressed = gamePadState.Buttons.B == ButtonState.Pressed;
        }
    }
}
