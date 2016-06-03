namespace DioLive.Xna.Controls
{
	using Helpers;
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework.Input;
	using System.Collections.Generic;       /// <summary>
						/// This is the main type for your game.
						/// </summary>
	public class Game1 : Game
	{
		private readonly GraphicsDeviceManager graphics;
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
			spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here
			Assets.Instance.Load(this);
			this.c1 = new Container(new Vector2(50, 50),
						new Vector2(400, 380),
						Color.Orange);
			var c2 = new Container(new Vector2(350, 100),
						new Vector2(200, 200),
						Color.LightBlue);
			this.c1.Items.Add(c2);
			var c3 = new Container(new Vector2(370, 120),
						new Vector2(200, 200),
						Color.Red);
			c2.Items.Add(c3);
			var c4 = new Container(new Vector2(350, 180),
						new Vector2(200, 200),
						Color.Yellow);
			this.c1.Items.Add(c4);
			var c5 = new Container(new Vector2(370, 190),
						new Vector2(200, 200),
						Color.Purple);
			c4.Items.Add(c5);
			this.c1.Items.Add(c4);
			var c6 = new TextBox(":",
						new Vector2(50, 60),
						new Vector2(350, 40),
						Color.Aqua); // TODO resolve conflict
			c6.SetTextures(new Dictionary<VisibleState, Texture2D>
			{
				{ VisibleState.Normal, Texture2DHelper.Generate(this.GraphicsDevice, (int)c6.Width, (int)c6.Height, Color.Pink)},
				{ VisibleState.Hover, Texture2DHelper.Generate(this.GraphicsDevice, (int)c6.Width, (int)c6.Height, Color.SkyBlue, 3, Color.Black)},
				{ VisibleState.Pressed, Texture2DHelper.Generate(this.GraphicsDevice, (int)c6.Width, (int)c6.Height, Color.SkyBlue, 3, Color.Red)},
			});
			this.c1.Items.Add(c6);
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

			c1.Update(gameTime);

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