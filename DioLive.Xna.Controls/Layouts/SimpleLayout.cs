using System;

using Microsoft.Xna.Framework;

namespace DioLive.Xna.Controls.Layouts
{
    public class SimpleLayout : ILayout
    {
        private readonly Container container;

        public SimpleLayout(Container container)
        {
            this.container = container;
        }

        public static LayoutBuilder GetBuilder() => (c) => new SimpleLayout(c);

        public Rectangle GetBounds(UIElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (!this.container.Elements.Contains(element))
            {
                throw new ArgumentException("Element was not found in this layout");
            }

            return new Rectangle(element.Location.ToPoint(), element.Size.ToPoint());
        }

        void ILayout.Invalidate()
        {
        }
    }
}