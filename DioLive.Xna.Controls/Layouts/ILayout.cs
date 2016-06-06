using Microsoft.Xna.Framework;

namespace DioLive.Xna.Controls.Layouts
{
	public interface ILayout
	{
		Rectangle GetBounds(UIElement element);

		void Invalidate();
	}
}