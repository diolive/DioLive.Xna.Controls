using System;

namespace DioLive.Xna.Interfaces
{
    public interface IHasFocus
    {
        event EventHandler MouseClick;

        event EventHandler MouseUnclick;

        event EventHandler MouseDown;

        event EventHandler MouseOut;

        event EventHandler MouseUp;

        bool IsFocused { get; set; }
    }
}