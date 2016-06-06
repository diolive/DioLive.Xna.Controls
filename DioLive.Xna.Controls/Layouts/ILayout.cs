namespace DioLive.Xna.Controls.Layouts
{
	using Microsoft.Xna.Framework;

	public interface ILayout
	{
		Rectangle GetBounds(UIElement element);

		void Invalidate();
	}
}