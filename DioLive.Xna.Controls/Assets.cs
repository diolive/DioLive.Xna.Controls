namespace DioLive.Xna.Controls
{
	using Algorithms.Extensions.Exceptions;
	using Helpers;
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;

	public static class Assets
	{
		public static Texture2D Pixel { get; private set; }

		public static RasterizerState Scissors { get; private set; }

		public static SpriteFont DefalutFont { get; private set; }
		public static Texture2D TextPtr { get; private set; }

		public static void Load(Game game)
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

			Assets.DefalutFont = game.Content.Load<SpriteFont>("Fonts/DefaultFont");
			Assets.TextPtr = Texture2DHelper.Generate(game.GraphicsDevice, 1, 10, Color.Black);
			Assets.Pixel = Texture2DHelper.Generate(game.GraphicsDevice, 1, 1, Color.White);
			Assets.Scissors = new RasterizerState { ScissorTestEnable = true };
		}
	}
}