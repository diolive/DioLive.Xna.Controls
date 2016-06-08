using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DioLive.Xna.Controls
{
    public class Background
    {
        public Background(Color color)
        {
            this.Color = color;
        }

        public Color Color { get; set; }

        public static implicit operator Background(Color color)
        {
            return new Background(color);
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle bounds)
        {
            if (spriteBatch == null)
            {
                throw new ArgumentNullException(nameof(spriteBatch));
            }

            spriteBatch.Draw(Assets.Pixel, bounds, this.Color);
        }
    }
}