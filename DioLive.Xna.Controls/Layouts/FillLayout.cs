using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;

namespace DioLive.Xna.Controls.Layouts
{
    public class FillLayout : ILayout
    {
        private readonly Container container;
        private readonly Direction direction;
        private readonly Dictionary<UIElement, Rectangle> bounds;

        public FillLayout(Container container, Direction direction = Direction.Horizontal)
        {
            this.container = container;
            this.direction = direction;
            this.bounds = new Dictionary<UIElement, Rectangle>();
        }

        public static LayoutBuilder GetBuilder(Direction direction = Direction.Horizontal) => (c) => new FillLayout(c, direction);

        public Rectangle GetBounds(UIElement element)
        {
            if (!this.container.Elements.Contains(element))
            {
                throw new ArgumentException("Element was not found in this layout");
            }

            return this.bounds[element];
        }

        public void Invalidate()
        {
            switch (this.direction)
            {
                case Direction.Horizontal:
                    this.InvalidateHorizontal();
                    break;

                case Direction.Vertical:
                    this.InvalidateVertical();
                    break;

                default:
                    throw new InvalidOperationException("Unknown fill style");
            }
        }

        private void InvalidateHorizontal()
        {
            this.bounds.Clear();

            Rectangle innerBounds = this.container.GetInnerBounds();
            UIElement[] elements = this.container.Elements.OrderBy(e => e.X).ToArray();
            float totalWidth = elements.Sum(e => e.Width);
            float shift = 0;

            foreach (var element in elements)
            {
                float elementWidth = element.Width / totalWidth * innerBounds.Width;
                this.bounds[element] = new Rectangle(innerBounds.Left + (int)Math.Round(shift),
                                                        innerBounds.Top,
                                                        (int)Math.Round(elementWidth),
                                                        innerBounds.Height);
                shift += elementWidth;
            }
        }

        private void InvalidateVertical()
        {
            this.bounds.Clear();

            Rectangle innerBounds = this.container.GetInnerBounds();
            UIElement[] elements = this.container.Elements.OrderBy(e => e.Y).ToArray();
            float totalHeight = elements.Sum(e => e.Height);
            float shift = 0;

            foreach (var element in elements)
            {
                float elementHeight = element.Height / totalHeight * innerBounds.Height;
                this.bounds[element] = new Rectangle(
                    innerBounds.Left,
                    innerBounds.Top + (int)Math.Round(shift),
                    innerBounds.Width,
                    (int)Math.Round(elementHeight));
                shift += elementHeight;
            }
        }

        public enum Direction : byte
        {
            Horizontal,
            Vertical,
        }
    }
}