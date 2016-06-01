using Microsoft.Xna.Framework;

namespace DioLive.Xna.Controls
{
    public class Background
    {
        public Color Color { get; set; }

        public static implicit operator Background(Color color)
        {
            return new Background { Color = color };
        }
    }
}