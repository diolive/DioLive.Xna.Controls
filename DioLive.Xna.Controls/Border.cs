namespace DioLive.Xna.Controls
{
	using Algorithms.Extensions.Exceptions;
	using Microsoft.Xna.Framework;

	public class Border
	{
		private int width;

		public Border(Color color, int width = 1)
		{
			this.Color = color;
			this.Width = width;
		}

		public Color Color { get; set; }

		public int Width
		{
			get
			{
				return this.width;
			}

			set
			{
				if (value < 0)
				{
					throw new ArgumentAppException("Border's width can not be negative");
				}

				this.width = value;
			}
		}
	}
}