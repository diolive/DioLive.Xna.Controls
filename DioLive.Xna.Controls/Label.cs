using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DioLive.Xna.Controls
{
    public class Label : UIElement
    {
        public Label()
        {
            Text = string.Empty;
        }

        public string Text { get; set; }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            //var gfx = spriteBatch.GraphicsDevice;

            //using (Scope.UseValue(() => gfx.RasterizerState, Assets.Scissors))
            {
                //using (Scope.UseValue(() => gfx.ScissorRectangle, Rectangle.Intersect(this.GetInnerBounds(), gfx.ScissorRectangle)))
                {
                    spriteBatch.DrawString(Assets.PTSans14, this.Text, new Vector2(30, 30), Color.Black);
                }
            }
        }
    }
}