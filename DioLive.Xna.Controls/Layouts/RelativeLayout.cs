﻿using System;

using Microsoft.Xna.Framework;

namespace DioLive.Xna.Controls.Layouts
{
    public class RelativeLayout : ILayout
    {
        private Container container;

        public RelativeLayout(Container container)
        {
            this.container = container;
        }

        public static LayoutBuilder GetBuilder() => (c) => new RelativeLayout(c);

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

            return new Rectangle((this.container.Location + element.Location).ToPoint(), element.Size.ToPoint());
        }

        void ILayout.Invalidate()
        {
        }
    }
}