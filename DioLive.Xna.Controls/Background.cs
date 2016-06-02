namespace DioLive.Xna.Controls
{
	using Microsoft.Xna.Framework;

	public class Background
	{
		public Color Color { get; set; }

		public Background(Color color)
		{
			this.Color = color;
		}

		public static implicit operator Background(Color color)
		{
			return new Background(color);
		}
	}
}