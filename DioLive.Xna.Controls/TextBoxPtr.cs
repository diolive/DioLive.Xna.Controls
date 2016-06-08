using Microsoft.Xna.Framework;

namespace DioLive.Xna.Controls
{
    internal class TextBoxPtr
    {
        internal TextBoxPtr() : this(Color.Black, 0)
        {
        }

        internal TextBoxPtr(Color color, uint textOffset)
        {
            this.Color = color;
            this.TextOffset = textOffset;
            this.Width = 1;
            this.Height = 30;
        }

        internal Color Color { get; set; }

        internal uint TextOffset { get; set; }

        internal int Height { get; set; }

        internal int Width { get; set; }
    }
}