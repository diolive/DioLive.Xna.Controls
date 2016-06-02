namespace DioLive.Xna.Controls
{
	using Algorithms.Extensions.Exceptions;
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using System.Collections.Generic;
	using System.Linq;

	public class Container : UIElement
	{
		public Container(Vector2 location, Vector2 size, Background background, Border border = null) : base(location, size, background, border)
		{
			this.Items = new List<UIElement>();
		}

		public IList<UIElement> Items { get; }

		public IEnumerable<UIElement> GetOrderedElements() => Items.OrderByDescending(e => e.ZOrder);

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			foreach (var element in this.GetOrderedElements())
			{
				element.Update(gameTime);
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (spriteBatch == null)
			{
				throw new ArgumentNullAppException("Sprite batch is null");
			}

			base.Draw(spriteBatch);
			var gfx = spriteBatch.GraphicsDevice;

			using (Scope.UseValue(() => gfx.RasterizerState, Assets.Instance.Scissors))
			{
				using (Scope.UseValue(() => gfx.ScissorRectangle, Rectangle.Intersect(this.InnerBounds, gfx.ScissorRectangle)))
				{
					foreach (var element in this.GetOrderedElements())
					{
						element.Draw(spriteBatch);
					}
				}
			}
		}
	}
}