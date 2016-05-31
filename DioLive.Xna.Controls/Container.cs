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

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);

            var spriteBatch = new SpriteBatch(sb.GraphicsDevice);
            //spriteBatch.End();

            RasterizerState currentRasterizerState = spriteBatch.GraphicsDevice.RasterizerState;
            Rectangle currentRect = spriteBatch.GraphicsDevice.ScissorRectangle;


            spriteBatch.Begin(SpriteSortMode.Immediate);

            spriteBatch.GraphicsDevice.RasterizerState = Assets.Scissors;
            spriteBatch.GraphicsDevice.ScissorRectangle = Rectangle.Intersect(this.Bounds, currentRect);

            foreach (var element in this.GetOrderedElements())
            {
                element.Draw(spriteBatch);
            }

            spriteBatch.End();

            spriteBatch.GraphicsDevice.ScissorRectangle = currentRect;
            spriteBatch.GraphicsDevice.RasterizerState = currentRasterizerState;

            //spriteBatch.Begin();
        }
    }
}