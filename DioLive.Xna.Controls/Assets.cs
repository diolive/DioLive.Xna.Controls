namespace DioLive.Xna.Controls
{
	using Algorithms.Extensions;
	using Algorithms.Extensions.Exceptions;
	using Helpers;
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using System;

	public class Assets : IDisposable
	{
		#region singleton

		protected Assets() { }

		public static Assets Instance
		{
			get
			{
				return SingletonCreator<Assets>.CreatorInstance;
			}
		}

		#endregion singleton

		public Texture2D Pixel { get; private set; }

		public RasterizerState Scissors { get; private set; }

		public void Dispose()
		{
			this.Scissors.Dispose();
			GC.SuppressFinalize(this);
		}

		public void Load(GraphicsDevice graphicsDevice)
		{
			if (graphicsDevice == null)
			{
				throw new ArgumentNullAppException("Graphic's device is null");
			}

			this.Pixel = Texture2DHelper.Generate(graphicsDevice, 1, 1, Color.White);
			this.Scissors = new RasterizerState { ScissorTestEnable = true };
		}
	}
}