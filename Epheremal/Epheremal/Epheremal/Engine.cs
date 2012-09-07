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

namespace Epheremal
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Engine : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Rectangle bounds;

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
            bounds = GraphicsDevice.Viewport.Bounds;
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
            // TODO: use this.Content to load your game content here
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
            interact();
            behaviour();

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
            Level level = new Level(1);
            level.LoadLevel(this);
            spriteBatch.Begin();
            spriteBatch = level.RenderLevel(ref spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        protected void interact()
        {

        }

        protected void behaviour()
        {

        }

        private void getInput()
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboardState = Keyboard.GetState();

            if (gamePadState.DPad.Left == ButtonState.Pressed || gamePadState.ThumbSticks.Left.X < 0 || keyboardState.IsKeyDown(Keys.Left))
            {

            }
            if (gamePadState.DPad.Right == ButtonState.Pressed || gamePadState.ThumbSticks.Left.X > 0 || keyboardState.IsKeyDown(Keys.Right))
            {

            }
            if (gamePadState.DPad.Down == ButtonState.Pressed || gamePadState.ThumbSticks.Left.Y < 0 || keyboardState.IsKeyDown(Keys.Down))
            {

            }
            if (gamePadState.DPad.Up == ButtonState.Pressed || gamePadState.Buttons.A == ButtonState.Pressed || gamePadState.ThumbSticks.Left.Y > 0 || keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.Space))
            {

            }

            if (gamePadState.Buttons.B == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.LeftShift))
            {

            }
        }
    }
}
