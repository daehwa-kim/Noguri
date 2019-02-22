/*
 * Program ID: DKimFinalProject - GameNoguri.cs
 * 
 * Purpose: To make a game using MonoGame
 * 
 * Revision History
 *  November 25, 2018 Dae Hwa Kim: Written
 *  
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace DKimFinalProject
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameNoguri : Game
    {
        static int gameWidth = 960;
        static int gameHeight = 600;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SoundEffect startupSound;
        int score = 0;
        int life = 3;
        const int LIFE_DEFAULT = 3;

        // Scenes
        private MainScene mainScene;
        private PlayScene playScene;
        private HelpScene helpScene;
        private AboutScene aboutScene;
        private ClearScene clearScene;
        private GameOverScene gameOverScene;

        public static int GameWidth { get => gameWidth; set => gameWidth = value; }
        public static int GameHeight { get => gameHeight; set => gameHeight = value; }
        public int Score { get => score; set => score = value; }
        public int Life { get => life; set => life = value; }
        internal ClearScene ClearScene { get => clearScene; set => clearScene = value; }
        internal GameOverScene GameOverScene { get => gameOverScene; set => gameOverScene = value; }

        /// <summary>
        /// Constructor
        /// </summary>
        public GameNoguri()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = gameWidth;
            graphics.PreferredBackBufferHeight = gameHeight;
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

            // Instantiate scenes
            mainScene = new MainScene(this, spriteBatch);
            this.Components.Add(mainScene);

            helpScene = new HelpScene(this, spriteBatch);
            this.Components.Add(helpScene);

            aboutScene = new AboutScene(this, spriteBatch);
            this.Components.Add(aboutScene);

            clearScene = new ClearScene(this, spriteBatch);
            this.Components.Add(clearScene);

            gameOverScene = new GameOverScene(this, spriteBatch);
            this.Components.Add(gameOverScene);

            // Load sounds
            startupSound = 
                this.Content.Load<SoundEffect>("Sounds/startup");

            // Show main scene
            startupSound.Play();
            mainScene.Show();
        }

        /// <summary>
        /// Create a new play scene with the stage number
        /// </summary>
        /// <param name="stage">Stage number to open</param>
        public void NewPlayScene(int stage = 1, bool keepScore = false)
        {
            // Delete existing play scene
            if (playScene != null)
            {
                playScene.Hide();
                playScene.Enabled = false;
                playScene.Dispose();
            }

            if (!keepScore)
            {
                score = 0;
            }

            // Create a new play scene
            playScene = new PlayScene(this, spriteBatch, stage);
            this.Components.Add(playScene);
            playScene.Show();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            // TODO: Add your update logic here

            KeyboardState ks = Keyboard.GetState();
            int selectedIndex = 0;

            if (mainScene.Enabled)
            {
                selectedIndex = mainScene.Menu.SelectedIndex;

                if (ks.IsKeyDown(Keys.Enter))
                {
                    mainScene.Hide();
                    switch (selectedIndex)
                    {
                        case 0:
                            life = LIFE_DEFAULT;
                            NewPlayScene();
                            break;
                        case 1:
                            helpScene.Show();
                            break;
                        case 2:
                            aboutScene.Show();
                            break;
                        case 3:
                            Exit();
                            break;
                    }
                }
            }
            else
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    if (playScene != null)
                    {
                        playScene.Hide();
                    }
                    helpScene.Hide();
                    aboutScene.Hide();
                    clearScene.Hide();
                    gameOverScene.Hide();
                    mainScene.Show();
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
