using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DioLive.Xna.Controls
{
	public abstract class UIElement
	{
		public Background Background { get; set; }

		public Border Border { get; set; }

		public Vector2 Location { get; set; }

		public Vector2 Size { get; set; }

		public int ZOrder { get; set; }

		public float X => this.Location.X;

		public float Y => this.Location.Y;

		public float Width => this.Size.X;

		public float Height => this.Size.Y;

		public Rectangle Bounds => new Rectangle(this.Location.ToPoint(), this.Size.ToPoint());

		public Rectangle InnerBounds
		{
			get
			{
				var bounds = Bounds;
				if (Border != null && Border.Width > 0)
				{
					bounds.Inflate(-Border.Width, -Border.Width);
				}
				return bounds;
			}
		}

		public virtual void Update(GameTime gameTime)
		{
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			Rectangle bounds = this.Bounds;

			var border = this.Border;
			if (border != null && border.Width > 0)
			{
				// Top border
				spriteBatch.Draw(Assets.Pixel, new Rectangle(bounds.X, bounds.Y, bounds.Width, border.Width), border.Color);

				// Right border
				spriteBatch.Draw(Assets.Pixel, new Rectangle(bounds.X + bounds.Width - border.Width, bounds.Y, border.Width, bounds.Height), border.Color);

				// Bottom border
				spriteBatch.Draw(Assets.Pixel, new Rectangle(bounds.X, bounds.Y + bounds.Height - border.Width, bounds.Width, border.Width), border.Color);

				// Left border
				spriteBatch.Draw(Assets.Pixel, new Rectangle(bounds.X, bounds.Y, border.Width, bounds.Height), border.Color);
			}

			var background = this.Background;
			if (background != null)
			{
				spriteBatch.Draw(Assets.Pixel, this.InnerBounds, background.Color);
			}
		}
	}
}