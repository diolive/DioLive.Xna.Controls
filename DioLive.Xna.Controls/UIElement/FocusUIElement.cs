using DioLive.Xna.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace DioLive.Xna.Controls
{
    public enum VisibleState
    {
        Normal = 0,
        Pressed = 1,
        Hover = 2,
    }

    public class FocusUIElement : UIElement, IHasFocus
    {
        #region Properties

        public bool IsFocused { get; set; }

        #endregion Properties

        #region Events

        public event EventHandler MouseClick;

        public event EventHandler MouseDown;

        public event EventHandler MouseOut;

        public event EventHandler MouseUnclick;

        public event EventHandler MouseUp;

        protected virtual void OnMouseClick(EventArgs e)
        {
            this.MouseClick?.Invoke(this, e);
        }

        protected virtual void OnMouseDown(EventArgs e)
        {
            this.MouseDown?.Invoke(this, e);
        }

        protected virtual void OnMouseOut(EventArgs e)
        {
            this.MouseOut?.Invoke(this, e);
        }

        protected virtual void OnMouseUnclick(EventArgs e)
        {
            this.MouseUnclick?.Invoke(this, e);
        }

        protected virtual void OnMouseUp(EventArgs e)
        {
            this.MouseUp?.Invoke(this, e);
        }

        #endregion Events

        #region States

        protected MouseState currentMouseState;
        protected VisibleState currentVisibleState = VisibleState.Normal;
        protected KeyboardState previousKeyboardState;
        protected MouseState previousMouseState;
        protected VisibleState previousVisibleState = VisibleState.Normal;

        #endregion States

        #region Public methods

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            this.previousVisibleState = this.currentVisibleState;
            this.previousMouseState = this.currentMouseState;

            this.currentMouseState = Mouse.GetState();

            if (this.GetBounds().Contains(this.currentMouseState.X, this.currentMouseState.Y))
            {
                if (this.currentMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (this.previousMouseState.LeftButton == ButtonState.Released)
                    {
                        this.currentVisibleState = VisibleState.Pressed;
                        this.IsFocused = true;
                        this.OnMouseDown(EventArgs.Empty);
                    }
                    else
                    {
                        if (this.currentVisibleState != VisibleState.Pressed)
                        {
                            this.currentVisibleState = VisibleState.Hover;
                        }
                    }
                }
                else
                {
                    if (this.previousVisibleState == VisibleState.Pressed)
                    {
                        this.OnMouseClick(EventArgs.Empty);
                    }

                    this.currentVisibleState = VisibleState.Hover;
                }
            }
            else
            {
                if (this.previousVisibleState == VisibleState.Hover ||
                    this.previousVisibleState == VisibleState.Pressed)
                {
                    this.OnMouseOut(EventArgs.Empty);
                }

                if (this.currentMouseState.LeftButton == ButtonState.Pressed)
                {
                    this.IsFocused = false;
                    this.OnMouseUnclick(EventArgs.Empty);
                }

                this.currentVisibleState = VisibleState.Normal;
            }

            if (this.currentMouseState.LeftButton == ButtonState.Released &&
                this.previousVisibleState == VisibleState.Pressed)
            {
                this.OnMouseUp(EventArgs.Empty);
            }
        }

        #endregion Public methods
    }
}