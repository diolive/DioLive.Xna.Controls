using Microsoft.Xna.Framework;
using System;

namespace DioLive.Xna.Controls.Layouts
{
	public class SimpleLayout : ILayout
	{
		private Container container;

		public SimpleLayout(Container container)
		{
			this.container = container;
		}

		public Rectangle GetBounds(UIElement element)
		{
			if (!this.container.Elements.Contains(element))
			{
				throw new ArgumentException("Element was not found in this layout");
			}

			return new Rectangle(element.Location.ToPoint(), element.Size.ToPoint());
		}

		void ILayout.Invalidate()
		{
		}

		public static LayoutBuilder GetBuilder() => (c) => new SimpleLayout(c);
	}
}