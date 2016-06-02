namespace DioLive.Xna.Controls
{
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;

	public class Pane : Container
	{
		public Pane(Vector2 location, Vector2 size, Background background, Border border = null) : base(location, size, background, border)
		{
		}

		/// <summary>
		/// Text inside the element
		/// </summary>
		public string buttonText;

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
		}
	}
}