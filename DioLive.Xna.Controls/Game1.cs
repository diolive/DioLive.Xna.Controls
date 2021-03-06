﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DioLive.Xna.Controls
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Container c1;

        public Game1()
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
            Assets.Load(this.Content);
            this.c1 = new Container
            {
                Location = new Vector2(50, 50),
                Size = new Vector2(400, 380),
                Background = Color.Orange,
            };
            var c2 = new Container
            {
                Location = new Vector2(350, 100),
                Size = new Vector2(200, 200),
                Background = Color.LightBlue,
            };
            this.c1.Items.Add(c2);
            var c3 = new Container
            {
                Location = new Vector2(370, 120),
                Size = new Vector2(200, 200),
                Background = Color.Red,
            };
            c2.Items.Add(c3);
            var c4 = new Container
            {
                Location = new Vector2(350, 180),
                Size = new Vector2(200, 200),
                Background = Color.LightBlue,
            };
            this.c1.Items.Add(c4);
            var c5 = new Container
            {
                Location = new Vector2(370, 190),
                Size = new Vector2(200, 200),
                Background = Color.Red,
            };
            c4.Items.Add(c5);
            this.c1.Items.Add(c4);
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Immediate);
            c1.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}