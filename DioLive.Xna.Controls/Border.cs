using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DioLive.Xna.Controls
{
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
					throw new ArgumentException("Border width cannot be less than zero", nameof(Width));
				}

				this.width = value;
			}
		}

		public void Draw(SpriteBatch spriteBatch, Rectangle bounds)
		{
			if (this.width == 0)
			{
				return;
			}

			// Top border
			spriteBatch.Draw(Assets.Pixel, new Rectangle(bounds.Left, bounds.Top, bounds.Width, this.width), this.Color);

			// Right border
			spriteBatch.Draw(Assets.Pixel, new Rectangle(bounds.Right - this.width, bounds.Top, this.width, bounds.Height), this.Color);

			// Bottom border
			spriteBatch.Draw(Assets.Pixel, new Rectangle(bounds.Left, bounds.Bottom - this.width, bounds.Width, this.width), this.Color);

			// Left border
			spriteBatch.Draw(Assets.Pixel, new Rectangle(bounds.Left, bounds.Top, this.width, bounds.Height), this.Color);
		}
	}
}