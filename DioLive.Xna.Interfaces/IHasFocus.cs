using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DioLive.Xna.Interfaces
{
    public interface IHasFocus
    {
		 bool IsFocused { get; set; }


		 event EventHandler MouseClick;

		 event EventHandler MouseUnclick;

		 event EventHandler MouseDown;

		 event EventHandler MouseOut;

		 event EventHandler MouseUp;

		void OnMouseClick(EventArgs e);

		void OnMouseUnclick(EventArgs e);

		void OnMouseDown(EventArgs e);

		void OnMouseOut(EventArgs e);

		void OnMouseUp(EventArgs e);

	}
}
