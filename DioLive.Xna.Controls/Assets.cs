namespace DioLive.Xna.Controls
{
	using Algorithms.Extensions;
	using Algorithms.Extensions.Exceptions;
	using Helpers;
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using System;

	public class Assets : IDisposable
	{
		#region singleton

		protected Assets() { }

		public static Assets Instance
		{
			get
			{
				return SingletonCreator<Assets>.CreatorInstance;
			}
		}

		#endregion singleton

		public Texture2D Pixel { get; private set; }

		public RasterizerState Scissors { get; private set; }

		public SpriteFont DefalutFont { get; private set; }

		public void Dispose()
		{
			this.Scissors.Dispose();
			GC.SuppressFinalize(this);
		}

		public void Load(Game game)
		{
			if (game == null)
			{
				throw new ArgumentNullAppException("Game is null");
			}

			if (game.GraphicsDevice == null)
			{
				throw new ArgumentNullAppException("Graphic's device is null");
			}

			if (game.Content == null)
			{
				throw new ArgumentNullAppException("Game content is null");
			}

			this.DefalutFont = game.Content.Load<SpriteFont>("Fonts/DefaultFont");
			this.Pixel = Texture2DHelper.Generate(game.GraphicsDevice, 1, 1, Color.White);
			this.Scissors = new RasterizerState { ScissorTestEnable = true };
		}
	}
}