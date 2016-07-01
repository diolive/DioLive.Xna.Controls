using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using DioLive.Xna.Controls.Helpers;
using System.Collections.Generic;

namespace DioLive.Xna.Controls
{
    public static class Assets
    {
        public static Texture2D Pixel { get; private set; }

        //public static Texture2D TextPtr { get; private set; }

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

        public static void Load(ContentManager content, GraphicsDevice gfx)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            Assets.Pixel = Texture2DHelper.Generate(gfx, 1, 1, Color.White);
            //Assets.TextPtr = Texture2DHelper.Generate(gfx, 1, 10, Color.Black);
            Assets.PTSans8 = content.Load<SpriteFont>("fonts/PTSans8");
            Assets.PTSans10 = content.Load<SpriteFont>("fonts/PTSans10");
            Assets.PTSans12 = content.Load<SpriteFont>("fonts/PTSans12");
            Assets.PTSans14 = content.Load<SpriteFont>("fonts/PTSans14");
            Assets.PTSans16 = content.Load<SpriteFont>("fonts/PTSans16");
            Assets.PTSans18 = content.Load<SpriteFont>("fonts/PTSans18");
            Assets.PTSans24 = content.Load<SpriteFont>("fonts/PTSans24");
            Assets.PTSans36 = content.Load<SpriteFont>("fonts/PTSans36");
            Assets.PTSans48 = content.Load<SpriteFont>("fonts/PTSans48");
            Assets.Scissors = new RasterizerState { ScissorTestEnable = true };

            Assets.dict = new Dictionary<Point, Texture2D> (16);
            Assets.graphicsDevice = gfx;
        }

        public static void Unload()
        {
            Assets.Pixel.Dispose();
            Assets.Scissors.Dispose();

            foreach (var item in dict)
            {
                item.Value.Dispose();
            }
        }

        private static Dictionary<Point, Texture2D> dict;
        private static GraphicsDevice graphicsDevice;

        public static Texture2D GetTextPtrTexture(Point size)
            => Assets.GetTextPtrTexture(size, Color.Black);

        public static Texture2D GetTextPtrTexture(Point size, Color color)
        {
            Texture2D texture = null;

            if (Assets.dict.TryGetValue(size, out texture))
            {
                return texture;
            }

            texture = Texture2DHelper.Generate(graphicsDevice, size.X, size.Y, color);
            Assets.dict[size] = texture;
            return texture;
        }
    }
}