using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace DioLive.Xna.Controls
{
    public class Container : UIElement
    {
        public Container()
        {
            this.Items = new List<UIElement>();
            this.Border = new Border { Color = Color.Black, Width = 2 };
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
            base.Draw(spriteBatch);
            var gfx = spriteBatch.GraphicsDevice;

            using (Scope.UseValue(() => gfx.RasterizerState, Assets.Scissors))
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