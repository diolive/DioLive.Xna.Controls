using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DioLive.Xna.Controls
{
    public static class Assets
    {
        public static Texture2D Pixel { get; private set; }

        public static SpriteFont PTSans8 { get; private set; }

        public static SpriteFont PTSans10 { get; private set; }

        public static SpriteFont PTSans12 { get; private set; }

        public static SpriteFont PTSans14 { get; private set; }

        public static SpriteFont PTSans16 { get; private set; }

        public static SpriteFont PTSans18 { get; private set; }

        public static SpriteFont PTSans24 { get; private set; }

        public static SpriteFont PTSans36 { get; private set; }

        public static SpriteFont PTSans48 { get; private set; }

        public static RasterizerState Scissors { get; private set; }

        public static void Load(ContentManager content)
        {
            Pixel = content.Load<Texture2D>("textures/pixel");
            PTSans8 = content.Load<SpriteFont>("fonts/PTSans8");
            PTSans10 = content.Load<SpriteFont>("fonts/PTSans10");
            PTSans12 = content.Load<SpriteFont>("fonts/PTSans12");
            PTSans14 = content.Load<SpriteFont>("fonts/PTSans14");
            PTSans16 = content.Load<SpriteFont>("fonts/PTSans16");
            PTSans18 = content.Load<SpriteFont>("fonts/PTSans18");
            PTSans24 = content.Load<SpriteFont>("fonts/PTSans24");
            PTSans36 = content.Load<SpriteFont>("fonts/PTSans36");
            PTSans48 = content.Load<SpriteFont>("fonts/PTSans48");
            Scissors = new RasterizerState { ScissorTestEnable = true };
        }

        public static void Unload()
        {
            Pixel.Dispose();
            Scissors.Dispose();
        }
    }
}