﻿using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;

namespace DioLive.Xna.Controls.Layouts
{
    public class FillLayout : ILayout
    {
        private Container container;
        private FillStyles fillStyle;
        private Dictionary<UIElement, Rectangle> bounds;

        public FillLayout(Container container, FillStyles fillStyle = FillStyles.Horizontal)
        {
            this.container = container;
            this.fillStyle = fillStyle;
            this.bounds = new Dictionary<UIElement, Rectangle>();
        }

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
            switch (this.fillStyle)
            {
                case FillStyles.Horizontal:
                    this.InvalidateHorizontal();
                    break;

                case FillStyles.Vertical:
                    this.InvalidateVertical();
                    break;

                default:
                    throw new InvalidOperationException("Unknown fill style");
            }
        }

        private void InvalidateHorizontal()
        {
            this.bounds.Clear();

            Rectangle bounds = this.container.GetInnerBounds();
            UIElement[] elements = this.container.Elements.OrderBy(e => e.X).ToArray();
            float totalWidth = elements.Sum(e => e.Width);
            float shift = 0;

            foreach (var element in elements)
            {
                float elementWidth = element.Width / totalWidth * bounds.Width;
                this.bounds[element] = new Rectangle(
                    bounds.Left + (int)Math.Round(shift),
                    bounds.Top,
                    (int)Math.Round(elementWidth),
                    bounds.Height);
                shift += elementWidth;
            }
        }

        private void InvalidateVertical()
        {
            this.bounds.Clear();

            Rectangle bounds = this.container.GetInnerBounds();
            UIElement[] elements = this.container.Elements.OrderBy(e => e.Y).ToArray();
            float totalHeight = elements.Sum(e => e.Height);
            float shift = 0;

            foreach (var element in elements)
            {
                float elementHeight = element.Height / totalHeight * bounds.Height;
                this.bounds[element] = new Rectangle(
                    bounds.Left,
                    bounds.Top + (int)Math.Round(shift),
                    bounds.Width,
                    (int)Math.Round(elementHeight));
                shift += elementHeight;
            }
        }

        public static LayoutBuilder GetBuilder(FillStyles fillStyle = FillStyles.Horizontal) => (c) => new FillLayout(c, fillStyle);

        public enum FillStyles : byte
        {
            Horizontal,
            Vertical,
        }
    }
}