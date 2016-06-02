namespace DioLive.Xna.Controls
{
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework.Input;

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
		public string Text { get; set; } // TODO

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

		private readonly Keys[] keys = new Keys[]
		{
			Keys.Q,
			Keys.W,
			Keys.E,
			Keys.R,
			Keys.T,
			Keys.Y,
			Keys.U,
			Keys.I,
			Keys.O,
			Keys.P,
			Keys.A,
			Keys.S,
			Keys.D,
			Keys.F,
			Keys.G,
			Keys.H,
			Keys.J,
			Keys.K,
			Keys.L,
			Keys.Z,
			Keys.X,
			Keys.C,
			Keys.V,
			Keys.B,
			Keys.N,
			Keys.M,
			Keys.Back,
			Keys.Space,
			Keys.Separator
		};
		private KeyboardState previousKeyboardState;

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (this.IsFocused)
			{
				KeyboardState state = Keyboard.GetState();

				foreach (var key in keys)
				{
					if (state.IsKeyUp(key) && (this.previousKeyboardState.IsKeyDown(key)))
					{
						if (key == Keys.Back)
						{
							if (this.Text.Length > 1)
							{
								this.Text = this.Text.Remove(this.Text.Length - 1, 1);
							}
							continue;
						}
						if (key == Keys.Space)
						{
							this.Text += " ";
							continue;
						}

						this.Text += key;
					}
				}

				this.previousKeyboardState = state;
			}
		}
	}
}