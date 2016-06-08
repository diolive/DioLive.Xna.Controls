using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using DioLive.Helpers.Properties;

namespace DioLive.Xna.Controls
{
    public class Label : UIElement
    {
        private static readonly SpriteFont DefaultFont = Assets.PTSans14;
        private static readonly Color DefaultColor = Color.Black;

        #region Fields

        private string text;
        private SpriteFont font;
        private Color textColor;

        private Vector2 textSize;
        private Vector2 textPosition;

        #endregion Fields

        #region Constructor

        public Label()
        {
            this.text = string.Empty;
            this.font = DefaultFont;
            this.textColor = DefaultColor;

            this.MeasureText();

            this.LocationChanged += (s, e) => this.MeasureText();
        }

        #endregion Constructor

        #region Events

        public event PropertyChangingEventHandler<string> TextChanging;

        public event PropertyChangedEventHandler<string> TextChanged;

        public event PropertyChangingEventHandler<SpriteFont> FontChanging;

        public event PropertyChangedEventHandler<SpriteFont> FontChanged;

        public event PropertyChangingEventHandler<Color> TextColorChanging;

        public event PropertyChangedEventHandler<Color> TextColorChanged;

        #endregion Events

        #region Properties

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.SetProperty(() => Text, value, () =>
                {
                    this.text = value;
                    this.MeasureText();
                }, TextChanging, TextChanged);
            }
        }

        public SpriteFont Font
        {
            get
            {
                return this.font;
            }
            set
            {
                this.SetProperty(() => Font, value, () =>
                {
                    this.font = value;
                    this.MeasureText();
                }, FontChanging, FontChanged);
            }
        }

        public Color TextColor
        {
            get
            {
                return this.textColor;
            }
            set
            {
                this.SetProperty(() => TextColor, value, () =>
                {
                    this.textColor = value;
                }, TextColorChanging, TextColorChanged);
            }
        }

        #endregion Properties

        #region Public methods

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (spriteBatch == null)
            {
                throw new ArgumentNullException(nameof(spriteBatch));
            }

            base.Draw(spriteBatch);

            // TODO: this call is redundant but required because of parent changes
            this.MeasureText();

            spriteBatch.DrawString(this.font, this.text, this.textPosition, this.textColor);
        }

        #endregion Public methods

        #region Private methods

        private void MeasureText()
        {
            this.textSize = this.font.MeasureString(this.text);

            float margin = this.font.MeasureString(" ").X;
            Rectangle bounds = this.GetBounds();

            if (textSize.X + margin * 2 > bounds.Width)
            {
                bounds.Width = (int)Math.Round(textSize.X + margin * 2);
            }

            if (textSize.Y + margin * 2 > bounds.Height)
            {
                bounds.Height = (int)Math.Round(textSize.Y + margin * 2);
            }

            this.Size = bounds.Size.ToVector2();

            this.textPosition = bounds.Location.ToVector2() + new Vector2(margin, (bounds.Height - this.textSize.Y) / 2);
        }

        #endregion Private methods
    }
}