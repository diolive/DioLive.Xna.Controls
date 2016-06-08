using DioLive.Xna.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace DioLive.Xna.Controls
{
    public class TextBox : FocusUIElement, IHasFocus
    {
        private static readonly SpriteFont DefaultFont = Assets.PTSans14;

        #region Fields

        private string text;
        private Vector2 stringPosition;
        private Vector2 textPtrPosition;
        private SpriteFont font;

        #endregion Fields

        #region Constructors

        public TextBox()
            : this(string.Empty)
        {
        }

        public TextBox(string text)
        {
            if (text == null)
            {
                text = string.Empty;
            }

            this.Font = DefaultFont;
            this.Text = text;

            this.TextPtr = new TextBoxPtr(new Point(1, (int)Font.MeasureString("W").Y)); // 'W' is usual letter in font
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Updating of this property will recalculate property this.TextSize
        /// </summary>
        public SpriteFont Font
        {
            get
            {
                return this.font;
            }
            set
            {
                this.font = value;

                if (this.text != null)
                {
                    this.TextSize = Font.MeasureString(this.Text);
                }
            }
        }

        /// <summary>
        /// Well, padding is almost workable
        /// </summary>
        public Vector2 Padding { get; set; }

        /// <summary>
        /// Updating of this property will recalculate property this.TextSize
        /// </summary>
        public string Text // string is only type, that can converts to string quickly
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;

                if (this.Font != null)
                {
                    this.TextSize = Font.MeasureString(this.Text);
                }
            }
        }

        /// <summary>
        /// Text cursor position inside this text box
        /// </summary>
        internal TextBoxPtr TextPtr { get; private set; }

        /// <summary>
        /// This property autoupdates when this.Text or this.Font is changing.
        /// </summary>
        internal Vector2 TextSize { get; set; }

        #endregion Properties

        #region Public methods

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (spriteBatch == null)
            {
                throw new ArgumentNullException(nameof(spriteBatch));
            }

            Background background = this.Background;
            if (background != null)
            {
                spriteBatch.Draw(Assets.Pixel, this.GetInnerBounds(), background.Color);
            }

            // here I emulate css's attribute 'overflow:hidden'
            // all text outside this text box will be cutted
            using (Scope.UseValue(() => spriteBatch.GraphicsDevice.RasterizerState, Assets.Scissors))
            {
                using (Scope.UseValue(() => spriteBatch.GraphicsDevice.ScissorRectangle, Rectangle.Intersect(this.GetInnerBounds(), spriteBatch.GraphicsDevice.ScissorRectangle)))
                {
                    spriteBatch.Draw(Assets.GetTextPtrTexture(this.TextPtr.Size),
                                        new Rectangle(textPtrPosition.ToPoint(), this.TextPtr.Size),
                                        Color.White);

                    spriteBatch.DrawString(Font, this.Text, stringPosition, Color.Black);
                }
            }
        }

        public override string ToString()
        {
            return this.Text;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.IsFocused)
            {
                KeyboardState state = Keyboard.GetState();

                foreach (Keys key in TextBox.allowedKeys)
                {
                    // TODO how to do KeyRelease in another way? Have to?..
                    if (state.IsKeyUp(key) &&
                        (this.previousKeyboardState.IsKeyDown(key)))
                    {
                        // if you wanna add new case block here, ensure, that you added new key value to TextBox.allowedKeys array
                        switch (key)
                        {
                            case Keys.Left:
                                {
                                    if (this.TextPtr.Offset < this.Text.Length)
                                    {
                                        this.TextPtr.Offset++;
                                    }
                                    continue;
                                }

                            case Keys.Right:
                                {
                                    if (this.TextPtr.Offset > 0)
                                    {
                                        this.TextPtr.Offset--;
                                    }
                                    continue;
                                }

                            case Keys.Home:
                                {
                                    this.TextPtr.Offset = (uint)this.Text.Length;
                                    continue;
                                }

                            case Keys.End:
                                {
                                    this.TextPtr.Offset = 0;
                                    continue;
                                }

                            case Keys.Back:
                                {
                                    if ((this.Text.Length > 0) &&
                                        (this.TextPtr.Offset != this.Text.Length))
                                    {
                                        this.Text = this.Text.Remove(this.Text.Length - 1 - (int)this.TextPtr.Offset, 1); // string is only type, that can converts to string quickly
                                    }
                                    continue;
                                }
                        }

                        // TODO bug: Caps + '1' == '!', but has to be '1'
                        bool isUppercase = (state.IsKeyDown(Keys.LeftShift) ||
                                            state.IsKeyDown(Keys.RightShift)) ^
                                            System.Windows.Forms.Control.IsKeyLocked(System.Windows.Forms.Keys.CapsLock); // TODO to ask about ref

                        if (this.TextPtr.Offset == 0)
                        {
                            string output = this.Map(key, isUppercase);

                            // if this keymap exists
                            if (output != null)
                            {
                                this.Text += output; // string is only type, that can converts to string quickly
                            }
                        }
                        else
                        {
                            string newStr = this.Map(key, isUppercase);
                            this.Text = this.Text.Insert(this.Text.Length - (int)this.TextPtr.Offset, newStr); // TODO bullshit, rewrite
                        }
                    }
                }

                this.previousKeyboardState = state;
                RecalcAll();
            }
        }

        #endregion Public methods

        #region Private methods

        private void RecalcAll()
        {
            // exact in this order
            RecalcPadding();
            RecalcStringPosition();
            RecalcTextPointerPosition();
        }

        private void RecalcTextPointerPosition()
        {
            this.textPtrPosition = stringPosition;

            if (this.TextPtr.Offset == 0)
            {
                this.textPtrPosition.X += this.TextSize.X;
            }
            else
            {
                this.textPtrPosition.X += this.Font
                                                .MeasureString(this.Text
                                                                    .Substring(0, this.Text.Length - (int)this.TextPtr.Offset))
                                                .X; // TODO optimizate
            }
        }

        private void RecalcStringPosition()
        {
            Point location = this.GetInnerBounds().Location;
            this.stringPosition = location.ToVector2();
            this.stringPosition.X += this.Padding.X;
            this.stringPosition.Y += this.Padding.Y;

            if (this.TextSize.X > this.Size.X)
            {
                this.stringPosition.X -= (this.TextSize.X - this.Size.X);
                this.stringPosition.X -= this.Padding.X + 5;
            }
        }

        private void RecalcPadding()
        {
            Vector2 fontSize = Font.MeasureString(this.Text);

            // TODO fix padding's magic numbers
            this.Padding = new Vector2
            {
                X = Math.Abs((this.Size.X - fontSize.X) / 2),

                Y = Math.Abs((this.Size.Y - fontSize.Y) / 4),
            };
        }

        #endregion Private methods

        #region indiaismymother

        #region allowedkeys

        private static readonly Keys[] allowedKeys = new Keys[]
                        {
                            Keys.Q,
                            Keys.W,
                            Keys.E,
                            Keys.R,
                            Keys.T,
                            Keys.Y,
                            Keys.U,
                            Keys.I,
                            Keys.O,
                            Keys.P,
                            Keys.A,
                            Keys.S,
                            Keys.D,
                            Keys.F,
                            Keys.G,
                            Keys.H,
                            Keys.J,
                            Keys.K,
                            Keys.L,
                            Keys.Z,
                            Keys.X,
                            Keys.C,
                            Keys.V,
                            Keys.B,
                            Keys.N,
                            Keys.M,

                            ////////////////////////// controls
                            Keys.Back,		        // backspace
                            Keys.Space,		        // space
                            Keys.OemQuestion,	    // ?
                            Keys.OemPeriod,		    // .
                            Keys.OemComma,		    // ,
                            Keys.OemSemicolon,	    // ;
                            Keys.OemQuotes,		    // '
                            Keys.OemPipe,		    // \
                            Keys.OemOpenBrackets,	// [
                            Keys.OemCloseBrackets,	// ]
                            Keys.OemMinus,		    // -
                            Keys.OemPlus,		    // =
                            Keys.OemTilde,		    // `
                            Keys.Left,		        // left arrow
                            Keys.Right,		        // right arrow
                            Keys.Home,              // home
                            Keys.End,               // end
                            //////////////////////////

                            /////////// arabic numbers
                            Keys.D0, //
                            Keys.D1, //
                            Keys.D2, //
                            Keys.D3, //
                            Keys.D4, //
                            Keys.D5, //
                            Keys.D6, //
                            Keys.D7, //
                            Keys.D8, //
                            Keys.D9, //
                            ///////////
                        };

        #endregion allowedkeys

        private string Map(Keys key, bool isShift)
        {
            string output = string.Empty;

            if (isShift)
            {
                #region isShift

                switch (key)
                {
                    case Keys.LeftShift:
                    case Keys.RightShift:
                    case Keys.Left:
                    case Keys.Right:

                    case Keys.Back:
                        {
                            if (output.Length > 0)
                            {
                                output = output.Remove(output.Length - 1, 1);
                            }
                            break;
                        }
                    case Keys.Space:
                        {
                            output = " ";
                            break;
                        }

                    case Keys.OemQuestion:          // ?
                        {
                            output = "?";
                            break;
                        }

                    case Keys.OemPeriod:            // .
                        {
                            output = ">";
                            break;
                        }

                    case Keys.OemComma:             // ,
                        {
                            output = "<";
                            break;
                        }

                    case Keys.OemSemicolon:         // ;
                        {
                            output = ":";
                            break;
                        }

                    case Keys.OemQuotes:            // '
                        {
                            output = "\"";
                            break;
                        }

                    case Keys.OemPipe:              // \
                        {
                            output = "|";
                            break;
                        }

                    case Keys.OemOpenBrackets:      // [
                        {
                            output = "{";
                            break;
                        }

                    case Keys.OemCloseBrackets:     // ]
                        {
                            output = "}";
                            break;
                        }

                    case Keys.OemMinus:             // -
                        {
                            output = "_";
                            break;
                        }

                    case Keys.OemPlus:              // =
                        {
                            output = "+";
                            break;
                        }

                    case Keys.OemTilde:             // `
                        {
                            output = "~";
                            break;
                        }

                    #region numbers

                    case Keys.D0:
                        {
                            output = ")";
                            break;
                        }
                    case Keys.D1:
                        {
                            output = "!";
                            break;
                        }
                    case Keys.D2:
                        {
                            output = "@";
                            break;
                        }
                    case Keys.D3:
                        {
                            output = "#";
                            break;
                        }
                    case Keys.D4:
                        {
                            output = "$";
                            break;
                        }
                    case Keys.D5:
                        {
                            output = "%";
                            break;
                        }
                    case Keys.D6:
                        {
                            output = "^";
                            break;
                        }
                    case Keys.D7:
                        {
                            output = "&";
                            break;
                        }
                    case Keys.D8:
                        {
                            output = "*";
                            break;
                        }
                    case Keys.D9:
                        {
                            output = "(";
                            break;
                        }

                    #endregion numbers

                    #region letters

                    case Keys.Q:
                        output = "Q";
                        break;

                    case Keys.W:
                        output = "W";
                        break;

                    case Keys.E:
                        output = "E";
                        break;

                    case Keys.R:
                        output = "R";
                        break;

                    case Keys.T:
                        output = "T";
                        break;

                    case Keys.Y:
                        output = "Y";
                        break;

                    case Keys.U:
                        output = "U";
                        break;

                    case Keys.I:
                        output = "I";
                        break;

                    case Keys.O:
                        output = "O";
                        break;

                    case Keys.P:
                        output = "P";
                        break;

                    case Keys.A:
                        output = "A";
                        break;

                    case Keys.S:
                        output = "S";
                        break;

                    case Keys.D:
                        output = "D";
                        break;

                    case Keys.F:
                        output = "F";
                        break;

                    case Keys.G:
                        output = "G";
                        break;

                    case Keys.H:
                        output = "H";
                        break;

                    case Keys.J:
                        output = "J";
                        break;

                    case Keys.K:
                        output = "K";
                        break;

                    case Keys.L:
                        output = "L";
                        break;

                    case Keys.Z:
                        output = "Z";
                        break;

                    case Keys.X:
                        output = "X";
                        break;

                    case Keys.C:
                        output = "C";
                        break;

                    case Keys.V:
                        output = "V";
                        break;

                    case Keys.B:
                        output = "B";
                        break;

                    case Keys.N:
                        output = "N";
                        break;

                    case Keys.M:
                        output = "M";
                        break;

                    #endregion letters

                    default:
                        output = null;
                        break;
                }

                #endregion isShift
            }
            else
            {
                #region notShift

                switch (key)
                {
                    case Keys.LeftShift:
                    case Keys.RightShift:
                    case Keys.Left:
                    case Keys.Right:
                        break;

                    case Keys.Back:
                        {
                            if (output.Length > 0)
                            {
                                output = output.Remove(output.Length - 1, 1);
                            }
                            break;
                        }
                    case Keys.Space:
                        {
                            output = " ";
                            break;
                        }
                    case Keys.OemQuestion:          // ?
                        {
                            output = "/";
                            break;
                        }

                    case Keys.OemPeriod:            // .
                        {
                            output = ".";
                            break;
                        }

                    case Keys.OemComma:             // ,
                        {
                            output = ",";
                            break;
                        }

                    case Keys.OemSemicolon:         // ;
                        {
                            output = ";";
                            break;
                        }

                    case Keys.OemQuotes:            // '
                        {
                            output = "'";
                            break;
                        }

                    case Keys.OemPipe:              // \
                        {
                            output = "\\";
                            break;
                        }

                    case Keys.OemOpenBrackets:      // [
                        {
                            output = "[";
                            break;
                        }

                    case Keys.OemCloseBrackets:     // ]
                        {
                            output = "]";
                            break;
                        }

                    case Keys.OemMinus:             // -
                        {
                            output = "-";
                            break;
                        }

                    case Keys.OemPlus:              // =
                        {
                            output = "=";
                            break;
                        }

                    case Keys.OemTilde:             // `
                        {
                            output = "`";
                            break;
                        }

                    #region numbers

                    case Keys.D0:
                        {
                            output = "0";
                            break;
                        }
                    case Keys.D1:
                        {
                            output = "1";
                            break;
                        }
                    case Keys.D2:
                        {
                            output = "2";
                            break;
                        }
                    case Keys.D3:
                        {
                            output = "3";
                            break;
                        }
                    case Keys.D4:
                        {
                            output = "4";
                            break;
                        }
                    case Keys.D5:
                        {
                            output = "5";
                            break;
                        }
                    case Keys.D6:
                        {
                            output = "6";
                            break;
                        }
                    case Keys.D7:
                        {
                            output = "7";
                            break;
                        }
                    case Keys.D8:
                        {
                            output = "8";
                            break;
                        }
                    case Keys.D9:
                        {
                            output = "9";
                            break;
                        }

                    #endregion numbers

                    #region letters

                    case Keys.Q:
                        output = "q";
                        break;

                    case Keys.W:
                        output = "w";
                        break;

                    case Keys.E:
                        output = "e";
                        break;

                    case Keys.R:
                        output = "r";
                        break;

                    case Keys.T:
                        output = "t";
                        break;

                    case Keys.Y:
                        output = "y";
                        break;

                    case Keys.U:
                        output = "u";
                        break;

                    case Keys.I:
                        output = "i";
                        break;

                    case Keys.O:
                        output = "o";
                        break;

                    case Keys.P:
                        output = "p";
                        break;

                    case Keys.A:
                        output = "a";
                        break;

                    case Keys.S:
                        output = "s";
                        break;

                    case Keys.D:
                        output = "d";
                        break;

                    case Keys.F:
                        output = "f";
                        break;

                    case Keys.G:
                        output = "g";
                        break;

                    case Keys.H:
                        output = "h";
                        break;

                    case Keys.J:
                        output = "j";
                        break;

                    case Keys.K:
                        output = "k";
                        break;

                    case Keys.L:
                        output = "l";
                        break;

                    case Keys.Z:
                        output = "z";
                        break;

                    case Keys.X:
                        output = "x";
                        break;

                    case Keys.C:
                        output = "c";
                        break;

                    case Keys.V:
                        output = "v";
                        break;

                    case Keys.B:
                        output = "b";
                        break;

                    case Keys.N:
                        output = "n";
                        break;

                    case Keys.M:
                        output = "m";
                        break;

                    #endregion letters

                    default:
                        output = null;
                        break;
                }

                #endregion notShift
            }

            return output;
        }

        #endregion indiaismymother
    }
}