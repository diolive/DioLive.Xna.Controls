using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using DioLive.Xna.Controls.Layouts;

namespace DioLive.Xna.Controls
{
    public class Container : UIElement
    {
        private static readonly LayoutBuilder DefaultLayout = SimpleLayout.GetBuilder();

        private List<UIElement> elements;

        public Container()
            : this(DefaultLayout)
        {
        }

        public Container(LayoutBuilder layoutBuilder)
        {
            this.elements = new List<UIElement>();

            ApplyLayout(layoutBuilder);
        }

        public ILayout Layout { get; private set; }

        public IList<UIElement> Elements => this.elements.AsReadOnly();

        public IEnumerable<UIElement> GetOrderedElements() => this.elements.OrderByDescending(e => e.ZOrder);

        public void ApplyLayout(LayoutBuilder layoutBuilder)
        {
            this.Layout = layoutBuilder(this);
        }

        public void AddElement(UIElement element)
        {
            element.Parent = this;
            if (!this.elements.Contains(element))
            {
                this.elements.Add(element);
                this.Layout.Invalidate();
            }
        }

        public void RemoveElement(UIElement element)
        {
            if (this.elements.Contains(element))
            {
                this.elements.Remove(element);
                this.Layout.Invalidate();
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
    }
}