﻿namespace DioLive.Xna.Controls
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
						this.Text = Map(key, this.Text, state.IsKeyDown(Keys.LeftShift) || state.IsKeyDown(Keys.RightShift));
					}
				}

				this.previousKeyboardState = state;
			}
		}

		private string Map(Keys key, string text, bool isShift)
		{
			if (text == null)
			{
				return null;
			}

			string output = text.Clone() as string;

			if (isShift)
			{
				switch (key)
				{
					#region isShift

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

					#region numbers

					case Keys.D0:
						{
							output += ")";
							break;
						}
					case Keys.D1:
						{
							output += "!";
							break;
						}
					case Keys.D2:
						{
							output += "@";
							break;
						}
					case Keys.D3:
						{
							output += "#";
							break;
						}
					case Keys.D4:
						{
							output += "$";
							break;
						}
					case Keys.D5:
						{
							output += "%";
							break;
						}
					case Keys.D6:
						{
							output += "^";
							break;
						}
					case Keys.D7:
						{
							output += "&";
							break;
						}
					case Keys.D8:
						{
							output += "*";
							break;
						}
					case Keys.D9:
						{
							output += "(";
							break;
						}

					#endregion numbers

					#region letters

					case Keys.Q:
						output += "Q";
						break;

					case Keys.W:
						output += "W";
						break;

					case Keys.E:
						output += "E";
						break;

					case Keys.R:
						output += "R";
						break;

					case Keys.T:
						output += "T";
						break;

					case Keys.Y:
						output += "Y";
						break;

					case Keys.U:
						output += "U";
						break;

					case Keys.I:
						output += "I";
						break;

					case Keys.O:
						output += "O";
						break;

					case Keys.P:
						output += "P";
						break;

					case Keys.A:
						output += "A";
						break;

					case Keys.S:
						output += "S";
						break;

					case Keys.D:
						output += "D";
						break;

					case Keys.F:
						output += "F";
						break;

					case Keys.G:
						output += "G";
						break;

					case Keys.H:
						output += "H";
						break;

					case Keys.J:
						output += "J";
						break;

					case Keys.K:
						output += "K";
						break;

					case Keys.L:
						output += "L";
						break;

					case Keys.Z:
						output += "Z";
						break;

					case Keys.X:
						output += "X";
						break;

					case Keys.C:
						output += "C";
						break;

					case Keys.V:
						output += "V";
						break;

					case Keys.B:
						output += "B";
						break;

					case Keys.N:
						output += "N";
						break;

					case Keys.M:
						output += "M";
						break;

					#endregion letters

					default:
						output += key;
						break;
				}

				#endregion isShift
			}
			else
			{
				#region notShift

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

					#region numbers

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

					#endregion numbers

					#region letters

					case Keys.Q:
						output += "q";
						break;

					case Keys.W:
						output += "w";
						break;

					case Keys.E:
						output += "e";
						break;

					case Keys.R:
						output += "r";
						break;

					case Keys.T:
						output += "t";
						break;

					case Keys.Y:
						output += "y";
						break;

					case Keys.U:
						output += "u";
						break;

					case Keys.I:
						output += "i";
						break;

					case Keys.O:
						output += "o";
						break;

					case Keys.P:
						output += "p";
						break;

					case Keys.A:
						output += "a";
						break;

					case Keys.S:
						output += "s";
						break;

					case Keys.D:
						output += "d";
						break;

					case Keys.F:
						output += "f";
						break;

					case Keys.G:
						output += "g";
						break;

					case Keys.H:
						output += "h";
						break;

					case Keys.J:
						output += "j";
						break;

					case Keys.K:
						output += "k";
						break;

					case Keys.L:
						output += "l";
						break;

					case Keys.Z:
						output += "z";
						break;

					case Keys.X:
						output += "x";
						break;

					case Keys.C:
						output += "c";
						break;

					case Keys.V:
						output += "v";
						break;

					case Keys.B:
						output += "b";
						break;

					case Keys.N:
						output += "n";
						break;

					case Keys.M:
						output += "m";
						break;

					#endregion letters

					default:
						output += key;
						break;
				}

				#endregion notShift
			}

			return output;
		}
	}
}