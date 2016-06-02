namespace DioLive.Xna.Controls
{
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using System.Diagnostics;
	public class TextBox : UIElement
	{
		public TextBox(string text, Vector2 location, Vector2 size, Background background, Border border = null) : base(location, size, background, border)
		{
			if (text == null)
			{
				text = string.Empty;
			}

			this.Text = text;
			this.Font = Assets.Instance.DefalutFont;

			Vector2 fontSize = Font.MeasureString(this.Text);

			this.PaddingX = (this.X - fontSize.X < 0 ?
						5 :
						((int)(this.X - fontSize.X) / 2));

			this.PaddingY = (this.Y - fontSize.Y < 0 ?
						0 :
						((int)(this.Y - fontSize.Y) / 4)); // why 4? #diefrontenddie
		}

		public SpriteFont Font { get; set; }

		/// <summary>
		/// Text inside the element
		/// </summary>
		public string Text { get; set; }

		public int PaddingX { get; set; }
		public int PaddingY { get; set; }

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			Vector2 fontPosition = new Vector2
			{
				X = this.Location.X + this.PaddingX,
				Y = this.Location.Y + this.PaddingY
			};

			spriteBatch.DrawString(Font, this.Text, fontPosition, Color.Black);
		}

		public override string ToString()
		{
			return this.Text;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (this.IsFocused)
			{
				Debug.WriteLine("true");
			}
			else
			{
				Debug.WriteLine("false");
			}
		}
	}
}