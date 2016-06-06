namespace DioLive.Xna.Controls.Helpers
{
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;

	public static class Texture2DHelper
	{
		public static Texture2D Generate(GraphicsDevice device, int width, int height, Color color)
		{
			Texture2D texture = new Texture2D(device, width, height);
			Color[] data = new Color[width * height];

			for (int i = 0; i < data.Length; i++)
			{
				data[i] = color;
			}

			texture.SetData(data);

			return texture;
		}

		public static Texture2D Generate(GraphicsDevice device, int width, int height, Color textureColor, int borderThick, Color borderColor)
		{
			Texture2D texture = new Texture2D(device, width, height);

			Color[] data = new Color[width * height];

			for (int i = 0; i < data.Length; i++)
			{
				data[i] = textureColor;
			}

			// painting vertical borders

			for (int i = 0; i < data.Length; i = i + width)
			{
				for (int j = 0; j < borderThick; j++)
				{
					data[i + j] = borderColor;
				}

				if (i > 1)
				{
					for (int j = 0; j < borderThick; j++)
					{
						data[i - 1 - j] = borderColor;
					}
				}
			}

			// painting horisontal borders

			for (int j = 0; j < borderThick; j++)
			{
				var bias = j * width;

				for (int i = 0; i < height; i++)
				{
					data[i + bias] = borderColor;
					data[data.Length - i - 1 - j * width] = borderColor;
				}
			}

			//set the color
			texture.SetData(data);

			return texture;
		}
	}
}