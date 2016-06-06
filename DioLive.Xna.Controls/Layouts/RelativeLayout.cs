namespace DioLive.Xna.Controls.Layouts
{
	using Microsoft.Xna.Framework;
	using System;

	public class RelativeLayout : ILayout
	{
		private readonly Container container;

		public RelativeLayout(Container container)
		{
			this.container = container;
		}

		public Rectangle GetBounds(UIElement element)
		{
			if (!this.container.Elements.Contains(element))
			{
				throw new ArgumentException("Element was not found in this layout");
			}

			return new Rectangle((this.container.Location + element.Location).ToPoint(), element.Size.ToPoint());
		}

		void ILayout.Invalidate()
		{
		}

		public static LayoutBuilder GetBuilder() => (c) => new RelativeLayout(c);
	}
}