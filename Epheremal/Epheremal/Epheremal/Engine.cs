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
using Epheremal.Model.Interactions;

namespace Epheremal
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Engine : Microsoft.Xna.Framework.Game
    {

        enum GameState { MENU, PLAYING}
        GameState gameState = GameState.MENU;

        /*
         * Menus
         */
        Texture2D splash;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Rectangle Bounds;
        public static Player Player;
        public static int xOffset {get; set;}
        public static int yOffset {get; set;}

        public static bool triggetNextLevel = false;

        public static bool MarioControl = false;

        private Level _currentLevel;
        private bool _toggleKeyPressed;
        private bool _toggleButtonPressed;
        private bool _toggleControlPressed;
        public static bool Alert;


        bool loadedLevel = false;
        KeyboardState lastKeyBoard = Keyboard.GetState();


        TileMap tileMap;
        List<RawLevel> levels;
        int currentLevel = 0;

        AnimatedTexture animatedTexture;

        SpriteFont font;

        int frameRate = 0;
        int frameCounter = 0;
        TimeSpan elapsedTime = TimeSpan.Zero;
        bool test = false;

        protected Song song;
        protected Song song2;

        

        public Engine()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            animatedTexture = new AnimatedTexture( 4, 10);

            // Set device frame rate to 60 fps.
            TargetElapsedTime = TimeSpan.FromSeconds(1 / 60.0);
            Window.AllowUserResizing = true; //allow resize.
            Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);

        }

        void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            Engine.Bounds = GraphicsDevice.Viewport.Bounds;
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
            
            //LevelParser.ParseTextFile("test.level");


            ContentManager manager = new ContentManager(this.Services, "Content");
            splash = manager.Load<Texture2D>("splash");

            _currentLevel = new Level(1);

            tileMap = LevelParser.ParseTileMap(this, "tilemap", 32);

            /*
             * Add Levels
             */ 
            levels = new List<RawLevel>();
            levels.Add(LevelParser.ParseTextFile("../../../../EpheremalContent/jump.level"));
            levels.Add(LevelParser.ParseTextFile("../../../../EpheremalContent/test.level"));
            levels.Add(LevelParser.ParseTextFile("../../../../EpheremalContent/bounce.level"));
            loadedLevel = false;

            /*
             * Create Player
             */ 
            Player = new Player(tileMap, 557, 557);


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
            SoundEffects.sounds.Add("pickupcoin", Content.Load<SoundEffect>("pickupcoin").CreateInstance());
            //SoundEffects.sounds.Add("hurt", Content.Load<SoundEffect>("song").CreateInstance());

            song = Content.Load<Song>("song");
            song2 = Content.Load<Song>("song2");
            MediaPlayer.Volume = 0.2f;

            try
            {
                MediaPlayer.Play(song);
                
            }
            catch (InvalidOperationException)
            {
                System.Diagnostics.Debug.WriteLine("don't steal music >:(");
            }

            MediaPlayer.IsRepeating = true;
            //MediaPlayer.Play(song);


            font = Content.Load<SpriteFont>("basicFont");
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

            // Check if the game has been won
            if (triggetNextLevel == true) {
                loadNextLevel();
                triggetNextLevel = false;
            }

            if (test)
            {
                test = false; return;
            }
            else test = true;
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;



            if (gameState == GameState.MENU)
            {
                getInput();
            }


            else if (loadedLevel && gameState == GameState.PLAYING)
            {
                // Allows the game to exit
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    this.Exit();

                if (Player.isDead)
                {
                    startLevel(levels[currentLevel]);
                    //MediaPlayer.Play(song);
                }

                // TODO: Add your update logic here
                getInput();
                //Scroll the viewport left and right when the player moves into the quarter of the screen on either side of the viewport,
                //we stop scrolling when the offset is flush to the left hand side (0), or right hand side (total width minus viewport width)
                if ((Player.PosX - Engine.xOffset) > (3 * Bounds.Width / 4) && (Engine.xOffset < (_currentLevel.GetLevelWidthInPixels() - Bounds.Width)) && Player.XVel > 0)
                    xOffset += Convert.ToInt32(Player.XVel);
                if ((Player.PosX - Engine.xOffset) < (Bounds.Width / 4) && Engine.xOffset > 0 && Player.XVel < 0)
                    xOffset += Convert.ToInt32(Player.XVel);
                if ((Player.PosY - Engine.yOffset) > (3 * Bounds.Height / 4) && (Engine.yOffset < (_currentLevel.GetLevelHeightInPixels() - Bounds.Height)) && Player.YVel > 0)
                    yOffset += Convert.ToInt32(Player.YVel);
                if ((Player.PosY - Engine.yOffset) < (Bounds.Height / 4) && Engine.yOffset > 0 && Player.YVel < 0)
                    yOffset += Convert.ToInt32(Player.YVel);

                _currentLevel.movement();
                _currentLevel.interact();
                _currentLevel.behaviour();

                //frame rate counter stuff
                elapsedTime += gameTime.ElapsedGameTime;

                if (elapsedTime > TimeSpan.FromSeconds(1))
                {
                    elapsedTime -= TimeSpan.FromSeconds(1);
                    frameRate = frameCounter;
                    frameCounter = 0;
                }
                
            }
            animatedTexture.UpdateFrame(elapsed);

            base.Update(gameTime);
        }


        //reloads the current level
        private void startLevel(RawLevel level)
        {

            gameState = GameState.PLAYING;
            loadedLevel = false;
            Player.isDead = false;
            Player.PosX = Block.BLOCK_WIDTH;
            Player.PosY = Block.BLOCK_WIDTH;
            Player.XVel = 0;
            Player.YVel = 0;
            Player.XAcc = 0;
            Player.YAcc = 0;
            Engine.xOffset = 0;
            Entity.State = EntityState.GOOD;
            loadedLevel = _currentLevel.LoadLevel(this, level, tileMap);
        }

        private void reloadCurrentLevel(){
            startLevel(levels[currentLevel]);
        }

        private void loadNextLevel()
        {

            currentLevel++;

            if (currentLevel < levels.Count)
            {
                startLevel(levels[currentLevel]);
            }

            else {
                currentLevel = 0;
                setSplashScreen(); 
            }
        }


        private void setSplashScreen() {

            gameState = GameState.MENU;


        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //TEST THINGS
            if (gameState == GameState.PLAYING)
            {
                spriteBatch.Begin();
                spriteBatch = _currentLevel.RenderLevel(ref spriteBatch);
                DrawText();
                spriteBatch.End();
            }

            if (gameState == GameState.MENU)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(splash, Bounds, Color.White);
                spriteBatch.End();
            }


            base.Draw(gameTime);
        }


        private void DrawText()
        {
            
            spriteBatch.DrawString(font, "Score: "+Player.score, new Vector2(5, 5), Color.White);
            spriteBatch.DrawString(font, Player.lives+"", new Vector2(Engine.Bounds.Right - 180, 5), Color.White);
            spriteBatch.DrawString(font, "Lives Remaining", new Vector2(Engine.Bounds.Right- 150, 5), Color.White);
          
            frameCounter++;

            string fps = string.Format("fps: {0}", frameRate);
            spriteBatch.DrawString(font, "" + fps, new Vector2(Engine.Bounds.Right- 150, Engine.Bounds.Bottom-50), Color.White);

            string controlScheme = string.Format("control: {0}", MarioControl ? "Mario" : "Physics");
            spriteBatch.DrawString(font, controlScheme, new Vector2(Engine.Bounds.Right - 350, Engine.Bounds.Bottom - 50), Color.White);

        }

        private void getInput()
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboardState = Keyboard.GetState();

            /*
             * Listen for menu input
             */

            if (gameState == GameState.MENU)
            {
                if (keyboardState.IsKeyDown(Keys.Space) && lastKeyBoard.IsKeyUp(Keys.Space))
                    startLevel(levels[0]);

                if (keyboardState.IsKeyDown(Keys.Escape) && lastKeyBoard.IsKeyUp(Keys.Escape))
                    Exit();

            }

            /*
             * Listen for game input 
             */

            if(gameState == GameState.PLAYING)
            {

                if(keyboardState.IsKeyDown(Keys.Escape) && lastKeyBoard.IsKeyUp(Keys.Escape)){
                    setSplashScreen();
                }

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
                if (gamePadState.DPad.Up == ButtonState.Pressed || gamePadState.Buttons.A == ButtonState.Pressed || gamePadState.ThumbSticks.Left.Y > 0 || keyboardState.IsKeyDown(Keys.Up) || (keyboardState.IsKeyDown(Keys.Space) && lastKeyBoard.IsKeyUp(Keys.Space)))
                {
                    Player.jumping();
                }

                // Change world state
                
                if ((gamePadState.Buttons.B == ButtonState.Released && _toggleButtonPressed) || (keyboardState.IsKeyDown(Keys.LeftShift) && lastKeyBoard.IsKeyUp(Keys.LeftShift)))
                {
                    if (_currentLevel.ValidateToggle())
                        if (Entity.State == EntityState.GOOD)
                        {
                            Entity.State = EntityState.BAD;
                            //MediaPlayer.Play(song2);
                        }
                        else
                        {
                            Entity.State = EntityState.GOOD;
                            //MediaPlayer.Play(song);
                        }
                    else
                        Alert = true;
                }
                
                // Reset 
                if (keyboardState.IsKeyDown(Keys.R) && lastKeyBoard.IsKeyUp(Keys.R))
                {
                    reloadCurrentLevel();
                }

                if (keyboardState.IsKeyDown(Keys.N) && lastKeyBoard.IsKeyUp(Keys.N))
                {
                    loadNextLevel();
                }

                if (keyboardState.IsKeyUp(Keys.C) && _toggleControlPressed)
                {
                    MarioControl = !MarioControl;
                }

                _toggleKeyPressed = keyboardState.IsKeyDown(Keys.LeftShift);
                _toggleButtonPressed = gamePadState.Buttons.B == ButtonState.Pressed;
                _toggleControlPressed = keyboardState.IsKeyDown(Keys.C);

                

            }
            // Reset 
            if ( keyboardState.IsKeyDown(Keys.R))
            {
                reloadCurrentLevel();

            }

            lastKeyBoard = keyboardState;


        }

    }
}
