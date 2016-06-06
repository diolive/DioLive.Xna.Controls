namespace DioLive.Xna.Controls.Layouts
{
	using Algorithms.Extensions.Exceptions;
	using Microsoft.Xna.Framework;

	public class SimpleLayout : ILayout
	{
		private readonly Container container;

		public SimpleLayout(Container container)
		{
			this.container = container;
		}

		public Rectangle GetBounds(UIElement element)
		{
			if (!this.container.Elements.Contains(element))
			{
				throw new ArgumentAppException("Element was not found in this layout");
			}

			return new Rectangle(element.Location.ToPoint(), element.Size.ToPoint());
		}

		void ILayout.Invalidate()
		{
		}

		public static LayoutBuilder GetBuilder() => (c) => new SimpleLayout(c);
	}
}