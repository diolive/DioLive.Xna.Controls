﻿using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using DioLive.Helpers.Properties;
using DioLive.Xna.Controls.Layouts;
using System;

namespace DioLive.Xna.Controls
{
    public class Container : UIElement
    {
        private static readonly LayoutBuilder DefaultLayout = SimpleLayout.GetBuilder();

        private readonly List<UIElement> elements;

        public Container()
            : this(DefaultLayout)
        {
        }

        public Container(LayoutBuilder layoutBuilder)
        {
            this.elements = new List<UIElement>();

            ApplyLayout(layoutBuilder);

            this.LocationChanged += (s, e) => this.Layout.Invalidate();
            this.SizeChanged += (s, e) => this.Layout.Invalidate();
        }

        public ILayout Layout { get; private set; }

        public IList<UIElement> Elements => this.elements.AsReadOnly();

        public IEnumerable<UIElement> GetOrderedElements() => this.elements.OrderByDescending(e => e.ZOrder);

        public void ApplyLayout(LayoutBuilder layoutBuilder)
        {
            this.Layout = layoutBuilder?.Invoke(this);
        }

        public void AddElement(UIElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            element.Parent = this;
            if (!this.elements.Contains(element))
            {
                this.elements.Add(element);
                this.Layout.Invalidate();

                element.LocationChanged += Element_PropertyChanged;
                element.SizeChanged += Element_PropertyChanged;
                element.ZOrderChanged += Element_PropertyChanged;
            }
        }

        public void RemoveElement(UIElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (this.elements.Contains(element))
            {
                this.elements.Remove(element);
                this.Layout.Invalidate();

                element.LocationChanged -= Element_PropertyChanged;
                element.SizeChanged -= Element_PropertyChanged;
                element.ZOrderChanged -= Element_PropertyChanged;
            }
        }

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
                throw new ArgumentNullException(nameof(spriteBatch));
            }

            base.Draw(spriteBatch);

            var gfx = spriteBatch.GraphicsDevice;

            using (Scope.UseValue(() => gfx.RasterizerState, Assets.Scissors))
            {
                using (Scope.UseValue(() => gfx.ScissorRectangle, Rectangle.Intersect(this.GetInnerBounds(), gfx.ScissorRectangle)))
                {
                    foreach (var element in this.GetOrderedElements())
                    {
                        element.Draw(spriteBatch);
                    }
                }
            }
        }

        private void Element_PropertyChanged<T>(object sender, PropertyChangedEventArgs<T> eventArgs)
        {
            this.Layout.Invalidate();
        }
    }
}