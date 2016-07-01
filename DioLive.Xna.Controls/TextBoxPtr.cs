using Microsoft.Xna.Framework;

namespace DioLive.Xna.Controls
{
    internal class TextBoxPtr
    {
        private Point size;

        internal TextBoxPtr(Point size) : this(size, Color.Black, 0)
        {
        }

        internal TextBoxPtr(Point size, Color color, uint textOffset)
        {
            this.Color = color;
            this.Offset = textOffset;
            this.Size = size; // TODO
        }

        internal Color Color { get; set; }

        internal uint Offset { get; set; }

        // this suppressed due to X and Y props
        internal Point Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
            }
        }

        internal int Y
        {
            get
            {
                return this.size.Y;
            }
            set
            {
                this.size.Y = value;
            }
        }

        internal int X
        {
            get
            {
                return this.size.X;
            }
            set
            {
                this.size.X = value;
            }
        }
    }
}