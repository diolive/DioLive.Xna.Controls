using Algorithms.Extensions.Exceptions;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using System;
    using System.Collections.Generic;

namespace DioLive.Xna.Controls
{
    public abstract class UIElement
    {
        #region Fields

        private float x;
        private float y;
        private float width;
        private float height;
        private int zOrder;

        #endregion Fields

        #region Events

        public event PropertyChangingEventHandler<Vector2> LocationChanging;

        public event PropertyChangedEventHandler<Vector2> LocationChanged;

        public event PropertyChangingEventHandler<Vector2> SizeChanging;

        public event PropertyChangedEventHandler<Vector2> SizeChanged;

        public event PropertyChangingEventHandler<int> ZOrderChanging;

        public event PropertyChangedEventHandler<int> ZOrderChanged;

        #endregion Events

        #region Properties

        public Background Background { get; set; }

		private Vector2 location;
		private Vector2 size;

        public float X
        {
            get
            {
                return this.x;
            }
            set
            {
                Vector2 oldValue = this.Location;
                Vector2 newValue = new Vector2(value, this.y);

                if (OnLocationChanging(oldValue, newValue))
                {
                    return;
                }

                this.x = value;

                OnLocationChanged(oldValue, newValue);
            }
        }

        public float Y
        {
            get
            {
                return this.y;
            }
            set
            {
                Vector2 oldValue = this.Location;
                Vector2 newValue = new Vector2(this.x, value);

                if (OnLocationChanging(oldValue, newValue))
                {
                    return;
                }

                this.y = value;

                OnLocationChanged(oldValue, newValue);
            }
        }

        public float Width
        {
            get
            {
                return this.width;
            }
            set
            {
                Vector2 oldValue = this.Size;
                Vector2 newValue = new Vector2(value, this.height);

                if (OnSizeChanging(oldValue, newValue))
                {
                    return;
                }

                this.width = value;

                OnSizeChanged(oldValue, newValue);
            }
        }

        public float Height
        {
            get
            {
                return this.height;
            }
            set
            {
                Vector2 oldValue = this.Size;
                Vector2 newValue = new Vector2(this.width, value);

                if (OnSizeChanging(oldValue, newValue))
                {
                    return;
                }

                this.height = value;

                OnSizeChanged(oldValue, newValue);
            }
        }

        public Vector2 Location
        {
            get
            {
                return new Vector2(this.x, this.y);
            }
            set
            {
                Vector2 oldValue = this.Location;
                Vector2 newValue = value;

                if (OnLocationChanging(oldValue, newValue))
                {
                    return;
                }

                this.x = value.X;
                this.y = value.Y;

                OnLocationChanged(oldValue, newValue);
            }
        }

        public Vector2 Size
        {
            get
            {
                return new Vector2(this.width, this.height);
            }
            set
            {
                Vector2 oldValue = this.Size;
                Vector2 newValue = value;

                if (OnSizeChanging(oldValue, newValue))
                {
                    return;
                }

                this.width = value.X;
                this.height = value.Y;

                OnSizeChanged(oldValue, newValue);
            }
        }

        public int ZOrder
        {
            get
            {
                return this.zOrder;
            }
            set
            {
                int oldValue = this.zOrder;
                int newValue = value;

                if (OnZOrderChanging(oldValue, newValue))
                {
                    return;
                }

                this.zOrder = value;

                OnZOrderChanged(oldValue, newValue);
            }
        }

        public Container Parent { get; set; }

        #endregion Properties

        #region Public methods

        public Rectangle GetBounds()
        {
            if (this.Parent == null)
            {
                return new Rectangle(this.Location.ToPoint(), this.Size.ToPoint());
            }

            return this.Parent.Layout.GetBounds(this);
        }

        public Rectangle GetInnerBounds()
        {
            Rectangle bounds = this.GetBounds();
            Border border = this.Border;

            if (border != null && border.Width > 0)
            {
                bounds.Inflate(-border.Width, -border.Width);
            }

            return bounds;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Rectangle bounds = GetBounds();
            Border border = this.Border;

            if (border != null)
            {
                border.Draw(spriteBatch, bounds);
                bounds = GetInnerBounds();
            }

            this.Background?.Draw(spriteBatch, bounds);
        }

        #endregion Public methods

        #region Protected methods

        protected virtual bool OnLocationChanging(Vector2 oldLocation, Vector2 newLocation)
        {
            var eventArgs = new PropertyChangingEventArgs<Vector2>(nameof(Location), oldLocation, newLocation);
            this.LocationChanging?.Invoke(this, eventArgs);
            return eventArgs.Cancel;
        }

        protected virtual void OnLocationChanged(Vector2 oldLocation, Vector2 newLocation)
        {
            var eventArgs = new PropertyChangedEventArgs<Vector2>(nameof(Location), oldLocation, newLocation);
            this.LocationChanged?.Invoke(this, eventArgs);
        }

        protected virtual bool OnSizeChanging(Vector2 oldSize, Vector2 newSize)
        {
            var eventArgs = new PropertyChangingEventArgs<Vector2>(nameof(Size), oldSize, newSize);
            this.SizeChanging?.Invoke(this, eventArgs);
            return eventArgs.Cancel;
        }

        protected virtual void OnSizeChanged(Vector2 oldSize, Vector2 newSize)
        {
            var eventArgs = new PropertyChangedEventArgs<Vector2>(nameof(Size), oldSize, newSize);
            this.SizeChanged?.Invoke(this, eventArgs);
        }

        protected virtual bool OnZOrderChanging(int oldZOrder, int newZOrder)
        {
            var eventArgs = new PropertyChangingEventArgs<int>(nameof(ZOrder), oldZOrder, newZOrder);
            this.ZOrderChanging?.Invoke(this, eventArgs);
            return eventArgs.Cancel;
        }

        protected virtual void OnZOrderChanged(int oldZOrder, int newZOrder)
        {
            var eventArgs = new PropertyChangedEventArgs<int>(nameof(ZOrder), oldZOrder, newZOrder);
            this.ZOrderChanged?.Invoke(this, eventArgs);
        }

        #endregion Protected methods
    }

}