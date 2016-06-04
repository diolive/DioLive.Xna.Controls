using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DioLive.Xna.Controls
{
    public static class Assets
    {
        public static Texture2D Pixel { get; private set; }

        public static RasterizerState Scissors { get; private set; }

        public static void Load(ContentManager content)
        {
            Pixel = content.Load<Texture2D>("pixel");
            Scissors = new RasterizerState { ScissorTestEnable = true };
        }

        public static void Unload()
        {
            Pixel.Dispose();
            Scissors.Dispose();
        }
    }
}