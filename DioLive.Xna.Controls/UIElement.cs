using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DioLive.Xna.Controls
{
    public abstract class UIElement
    {
        public Color Background { get; set; }

        public Vector2 Location { get; set; }

        public Vector2 Size { get; set; }

        public int ZOrder { get; set; }

        public float X => this.Location.X;

        public float Y => this.Location.Y;

        public float Width => this.Size.X;

        public float Height => this.Size.Y;

        public Rectangle Bounds => new Rectangle(this.Location.ToPoint(), this.Size.ToPoint());

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Assets.Pixel, this.Bounds, this.Background);
        }
    }
}