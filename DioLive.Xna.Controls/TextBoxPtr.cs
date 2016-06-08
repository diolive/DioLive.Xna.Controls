using Microsoft.Xna.Framework;

namespace DioLive.Xna.Controls
{
    internal class TextBoxPtr
    {
        private Point size;


        internal TextBoxPtr() : this(Color.Black, 0)
        {
        }

        internal TextBoxPtr(Color color, uint textOffset)
        {
            this.Color = color;
            this.TextOffset = textOffset;
            this.Size = new Point(1, 30);
        }

        internal Color Color { get; set; }

        internal uint TextOffset { get; set; }

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
                this.size.Y = 2;
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
                this.size.X = 2;
            }
        }
    }
}