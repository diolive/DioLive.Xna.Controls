using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DioLive.Xna.Controls
{
    public abstract class UIElement
    {
        public Background Background { get; set; }

        public Border Border { get; set; }

        public Vector2 Location { get; set; }

        public Vector2 Size { get; set; }

        public int ZOrder { get; set; }

        public Container Parent { get; set; }

        public float X => this.Location.X;

        public float Y => this.Location.Y;

        public float Width => this.Size.X;

        public float Height => this.Size.Y;

        public Rectangle GetBounds()
        {
            if (this.Parent == null)
            {
                return new Rectangle(this.Location.ToPoint(), this.Size.ToPoint());
            }

            return this.Parent.Layout.GetBounds(this);
        }

        public Rectangle GetInnerBounds()
        {
            Rectangle bounds = this.GetBounds();
            Border border = this.Border;

            if (border != null && border.Width > 0)
            {
                bounds.Inflate(-border.Width, -border.Width);
            }

            return bounds;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Rectangle bounds = GetBounds();
            Border border = this.Border;

            if (border != null)
            {
                border.Draw(spriteBatch, bounds);
                System.Diagnostics.Debug.WriteLine($"Border: {bounds}");
                bounds = GetInnerBounds();
            }

            this.Background?.Draw(spriteBatch, bounds);
            System.Diagnostics.Debug.WriteLine($"Background: {bounds}");
        }
    }
}