namespace DioLive.Xna.Controls
{
	using System;
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

			// arabic numbers
			Keys.D0,
			Keys.D1,
			Keys.D2,
			Keys.D3,
			Keys.D4,
			Keys.D5,
			Keys.D6,
			Keys.D7,
			Keys.D8,
			Keys.D9,
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
						this.Text = Map(key, this.Text);

					}
				}

				this.previousKeyboardState = state;
			}
		}

		private string Map(Keys key, string text)
		{
			if (text == null)
			{
				return null;
			}

			string output = text.Clone() as string;

			switch (key)
			{
				case Keys.Back:
					{
						if (output.Length > 1)
						{
							output = output.Remove(output.Length - 1, 1);
						}
						break;
					}
				case Keys.Space:
					{
						output += " ";
						break;
					}

				case Keys.D0:
					{
						output += "0";
						break;
					}
				case Keys.D1:
					{
						output += "1";
						break;
					}
				case Keys.D2:
					{
						output += "2";
						break;
					}
				case Keys.D3:
					{
						output += "3";
						break;
					}
				case Keys.D4:
					{
						output += "4";
						break;
					}
				case Keys.D5:
					{
						output += "5";
						break;
					}
				case Keys.D6:
					{
						output += "6";
						break;
					}
				case Keys.D7:
					{
						output += "7";
						break;
					}
				case Keys.D8:
					{
						output += "8";
						break;
					}
				case Keys.D9:
					{
						output += "9";
						break;
					}
				default:
					output += key;
					break;
			}

			return output;

		}
	}
}