using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DioLive.Xna.Controls
{
	internal class TextBoxPtr
	{
		public TextBoxPtr() : this(Color.Black, 0)
		{
		}

		public TextBoxPtr(Color color, uint textOffset)
		{
			this.Color = color;
			this.TextOffset = textOffset;
			this.Width = 1;
			this.Height = 30;
		}

		Color Color { get; set; }

		internal uint TextOffset { get; set; }
		public int Height { get; internal set; }
		public int Width { get; internal set; }

		internal Vector2 GetAbsolutePosition(TextBox textbox)
		{
			Vector2 result = new Vector2();

			result.X += textbox.X;
			result.Y += textbox.Y;

			result.X += textbox.Padding.X;
			result.Y += textbox.Padding.Y / 2; // TODO fix padding

			if (this.TextOffset == 0)
			{
				result.X += textbox.TextSize.X;
			}
			else
			{
				result.X += textbox.Font.MeasureString(textbox.Text.Substring(0, textbox.Text.Length - (int)this.TextOffset)).X;
			}

			return result;
		}
	}
}
