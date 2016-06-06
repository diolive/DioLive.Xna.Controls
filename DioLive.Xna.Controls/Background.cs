using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DioLive.Xna.Controls
{
	public class Background
	{
		public Background(Color color)
		{
			this.Color = color;
		}

		public Color Color { get; set; }

		public static implicit operator Background(Color color)
		{
			return new Background(color);
		}

		public void Draw(SpriteBatch spriteBatch, Rectangle bounds)
		{
			spriteBatch.Draw(Assets.Pixel, bounds, this.Color);
		}
	}
}