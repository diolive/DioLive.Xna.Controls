using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using DioLive.Helpers.Properties;

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
        private Background background;
        private Border border;
        private Container parent;

        #endregion Fields

        #region Events

        public event PropertyChangingEventHandler<Vector2> LocationChanging;

        public event PropertyChangedEventHandler<Vector2> LocationChanged;

        public event PropertyChangingEventHandler<Vector2> SizeChanging;

        public event PropertyChangedEventHandler<Vector2> SizeChanged;

        public event PropertyChangingEventHandler<int> ZOrderChanging;

        public event PropertyChangedEventHandler<int> ZOrderChanged;

        public event PropertyChangingEventHandler<Background> BackgroundChanging;

        public event PropertyChangedEventHandler<Background> BackgroundChanged;

        public event PropertyChangingEventHandler<Border> BorderChanging;

        public event PropertyChangedEventHandler<Border> BorderChanged;

        public event PropertyChangingEventHandler<Container> ParentChanging;

        public event PropertyChangedEventHandler<Container> ParentChanged;

        #endregion Events

        #region Properties

        public Background Background
        {
            get
            {
                return this.background;
            }
            set
            {
                this.SetProperty(() => this.Background, value, () =>
                {
                    this.background = value;
                }, BackgroundChanging, BackgroundChanged);
            }
        }

        public Border Border
        {
            get
            {
                return this.border;
            }
            set
            {
                this.SetProperty(() => Border, value, () =>
                {
                    this.border = value;
                }, BorderChanging, BorderChanged);
            }
        }

        public float X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.SetProperty(() => this.Location, new Vector2(value, this.y), () =>
                {
                    this.x = value;
                }, LocationChanging, LocationChanged);
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
                this.SetProperty(() => this.Location, new Vector2(this.x, value), () =>
                {
                    this.y = value;
                }, LocationChanging, LocationChanged);
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
                this.SetProperty(() => this.Size, new Vector2(value, this.height), () =>
                {
                    this.width = value;
                }, SizeChanging, SizeChanged);
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
                this.SetProperty(() => this.Size, new Vector2(this.width, value), () =>
                {
                    this.height = value;
                }, SizeChanging, SizeChanged);
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
                this.SetProperty(() => this.Location, value, () =>
                {
                    this.x = value.X;
                    this.y = value.Y;
                }, LocationChanging, LocationChanged);
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
                this.SetProperty(() => this.Size, value, () =>
                {
                    this.width = value.X;
                    this.height = value.Y;
                }, SizeChanging, SizeChanged);
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
                this.SetProperty(() => this.ZOrder, value, () =>
                {
                    this.zOrder = value;
                }, ZOrderChanging, ZOrderChanged);
            }
        }

        public Container Parent
        {
            get
            {
                return this.parent;
            }
            set
            {
                this.SetProperty(() => this.Parent, value, () =>
                {
                    this.parent = value;
                }, ParentChanging, ParentChanged);
            }
        }

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

            if (border != null &&
                    border.Width > 0)
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
    }
}