namespace DioLive.Xna.Controls
{
	using Helpers;
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework.Input;
	using System.Collections.Generic;

	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		private readonly GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;
		private Container mainContainer;

		public Game1()
		{
			this.graphics = new GraphicsDeviceManager(this);
			this.Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// Add your initialization logic here
			this.IsMouseVisible = true;

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

			// use this.Content to load your game content here
			Assets.Load(this);
			this.mainContainer = new Container(new Vector2(50, 50),
						new Vector2(400, 380),
						Color.Orange);
			Container c2 = new Container(new Vector2(350, 100),
						new Vector2(200, 200),
						Color.LightBlue);
			this.mainContainer.Items.Add(c2);
			Container c3 = new Container(new Vector2(370, 120),
						new Vector2(200, 200),
						Color.Red);
			c2.Items.Add(c3);
			Container c4 = new Container(new Vector2(350, 180),
						new Vector2(200, 200),
						Color.Yellow);
			this.mainContainer.Items.Add(c4);
			Container c5 = new Container(new Vector2(370, 190),
						new Vector2(200, 200),
						Color.Purple);
			c4.Items.Add(c5);
			this.mainContainer.Items.Add(c4);
			TextBox c6 = new TextBox(string.Empty,
						new Vector2(50, 60),
						new Vector2(100, 40),
						Color.Aqua); // TODO resolve conflict

			c6.SetTextures(new Dictionary<VisibleState, Texture2D>
			{
				{ VisibleState.Normal, Texture2DHelper.Generate(this.GraphicsDevice, (int)c6.Width, (int)c6.Height, Color.Pink)},
				{ VisibleState.Hover, Texture2DHelper.Generate(this.GraphicsDevice, (int)c6.Width, (int)c6.Height, Color.SkyBlue, 3, Color.Black)},
				{ VisibleState.Pressed, Texture2DHelper.Generate(this.GraphicsDevice, (int)c6.Width, (int)c6.Height, Color.SkyBlue, 3, Color.Red)},
			});
			this.mainContainer.Items.Add(c6);
		}

            // TODO: use this.Content to load your game content here
            Assets.Load(this.Content);
            this.c1 = new Container(RelativeLayout.GetBuilder())
            {
                Location = new Vector2(50, 50),
                Size = new Vector2(400, 380),
                Background = Color.Orange,
            };

            Label l1 = new Label
            {
                Location = new Vector2(10, 10),
                Size = new Vector2(200, 40),
                Background = Color.DarkGray,
                Text = "Hello",
            };


            this.c1.AddElement(l1);

            //var c2 = new Container
            //{
            //    Location = new Vector2(50, 50),
            //    Size = new Vector2(300, 300),
            //    Background = Color.LightBlue,
            //};
            //this.c1.AddElement(c2);
            //var c3 = new Container
            //{
            //    Location = new Vector2(370, 120),
            //    Size = new Vector2(200, 200),
            //    Background = Color.Red,
            //};
            ////c2.ApplyLayout(FillLayout.GetBuilder());
            //c2.AddElement(c3);
            //var c4 = new Container
            //{
            //    Location = new Vector2(250, 50),
            //    Size = new Vector2(2000, 200),
            //    Background = Color.Yellow,
            //};
            //c1.AddElement(c4);
            //var c5 = new Container
            //{
            //    Location = new Vector2(370, 190),
            //    Size = new Vector2(200, 200),
            //    Background = Color.Purple,
            //};
            //c4.AddElement(c5);
            ////this.c1.AddElement(c3);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            Assets.Unload();
        }
		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
				Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				this.Exit();
			}

			// Add your update logic here

			mainContainer.Update(gameTime);

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			this.GraphicsDevice.Clear(Color.CornflowerBlue);

			this.spriteBatch.Begin(SpriteSortMode.Immediate);
			mainContainer.Draw(spriteBatch);
			this.spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}