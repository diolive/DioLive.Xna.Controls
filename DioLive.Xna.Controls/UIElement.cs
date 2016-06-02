namespace DioLive.Xna.Controls
{
	using Algorithms.Extensions.Exceptions;
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework.Input;
	using System;
	using System.Collections.Generic;

	public enum VisibleState
	{
		Normal = 0,
		Pressed = 1,
		Hover = 2,
	}

	public abstract class UIElement
	{
		#region ctors

		protected UIElement(Vector2 location, Vector2 size, Background background, Border border = null)
		{
			this.Textures = new Dictionary<VisibleState, Texture2D>();

			this.Location = location;
			this.Size = size;
			this.Background = background;

			if (border == null)
			{
				border = new Border(Color.Black, 2);
			}

			this.Border = border;

			this.MouseClick += (s, e) => { this.IsFocused = true; };
			this.MouseUnclick += (s, e) => { this.IsFocused = false; };
		}

		#endregion ctors

		public Background Background { get; set; }
		public Border Border { get; set; }

		public Rectangle InnerBounds
		{
			get
			{
				var bounds = Bounds;
				if (Border != null)
				{
					bounds.Inflate(-Border.Width, -Border.Width);
				}
				return bounds;
			}
		}

		public Vector2 Location
		{
			get
			{
				return this.location;
			}
			set
			{
				if ((value.X < 0) ||
						(value.Y < 0))
				{
					throw new ArgumentAppException("Size of UI element can not be negative");
				}

				this.location = value;
			}
		}

		public Vector2 Size
		{
			get
			{
				return this.size;
			}
			set
			{
				if ((value.X < 0) ||
						(value.Y < 0))
				{
					throw new ArgumentAppException("Size of UI element can not be negative");
				}

				this.size = value;
			}
		}

		/// <summary>
		/// Init it with MonogameStock dictionaries, using SetTextures() method
		/// </summary>
		public Dictionary<VisibleState, Texture2D> Textures { get; private set; }

		public int ZOrder { get; set; }

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			if (spriteBatch == null)
			{
				throw new ArgumentNullAppException("Sprite batch is Scope.UseValue() is null");
			}

			Rectangle bounds = this.Bounds;

			var border = this.Border;
			if (border != null)
			{
				// Top border
				spriteBatch.Draw(Assets.Instance.Pixel, new Rectangle(bounds.X, bounds.Y, bounds.Width, border.Width), border.Color);

				// Right border
				spriteBatch.Draw(Assets.Instance.Pixel, new Rectangle(bounds.X + bounds.Width - border.Width, bounds.Y, border.Width, bounds.Height), border.Color);

				// Bottom border
				spriteBatch.Draw(Assets.Instance.Pixel, new Rectangle(bounds.X, bounds.Y + bounds.Height - border.Width, bounds.Width, border.Width), border.Color);

				// Left border
				spriteBatch.Draw(Assets.Instance.Pixel, new Rectangle(bounds.X, bounds.Y, border.Width, bounds.Height), border.Color);
			}

			if (this.Textures.Count > 0)
			{
				spriteBatch.Draw(this.Textures[currentVisibleState], this.Bounds, Color.White);
			}

			var background = this.Background;
			if (background != null)
			{
				spriteBatch.Draw(Assets.Instance.Pixel, this.InnerBounds, background.Color);
			}
		}

		public void SetTextures(Dictionary<VisibleState, Texture2D> textures)
		{
			this.Textures = textures;
		}

		public virtual void Update(GameTime gameTime)
		{
			this.previousVisibleState = this.currentVisibleState;
			this.previousMouseState = this.currentMouseState;

			this.currentMouseState = Mouse.GetState();

			if (this.Bounds.Contains(this.currentMouseState.X, this.currentMouseState.Y))
			{
				if (this.currentMouseState.LeftButton == ButtonState.Pressed)
				{
					if (this.previousMouseState.LeftButton == ButtonState.Released)
					{
						this.OnMouseDown(EventArgs.Empty);
						this.currentVisibleState = VisibleState.Pressed;
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
				//if (this.previousVisibleState == VisibleState.Hover ||
				//	this.previousVisibleState == VisibleState.Pressed)
				//{
				//	this.OnMouseOut(EventArgs.Empty);
				//}

				if (this.currentMouseState.LeftButton == ButtonState.Pressed)
				{
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

		protected MouseState currentMouseState;
		protected VisibleState currentVisibleState = VisibleState.Normal;
		protected MouseState previousMouseState;
		protected VisibleState previousVisibleState = VisibleState.Normal;

		private Vector2 location;
		private Vector2 size;

		#region geometry

		public Rectangle Bounds => new Rectangle(this.Location.ToPoint(), this.Size.ToPoint());
		public float Height => this.Size.Y;
		public float Width => this.Size.X;
		public float X => this.Location.X;
		public float Y => this.Location.Y;

		#endregion geometry

		public bool IsFocused { get; set; }

		#region events

		public event EventHandler MouseClick;

		public event EventHandler MouseUnclick;

		public event EventHandler MouseDown;

		public event EventHandler MouseOut;

		public event EventHandler MouseUp;

		public void OnMouseClick(EventArgs e)
		{
			this.MouseClick?.Invoke(this, e);
		}

		public void OnMouseUnclick(EventArgs e)
		{
			this.MouseUnclick?.Invoke(this, e);
		}

		public void OnMouseDown(EventArgs e)
		{
			this.MouseDown?.Invoke(this, e);
		}

		public void OnMouseOut(EventArgs e)
		{
			this.MouseOut?.Invoke(this, e);
		}

		public void OnMouseUp(EventArgs e)
		{
			this.MouseUp?.Invoke(this, e);
		}

		#endregion events
	}
}