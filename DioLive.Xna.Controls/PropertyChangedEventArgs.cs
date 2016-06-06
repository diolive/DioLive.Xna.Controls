namespace DioLive.Xna.Controls
{
	using System;

	public class PropertyChangedEventArgs<TValue> : EventArgs
	{
		public PropertyChangedEventArgs(string propertyName, TValue oldValue, TValue newValue)
		{
			this.PropertyName = propertyName;
			this.OldValue = oldValue;
			this.NewValue = newValue;
		}

		public string PropertyName { get; }

		public TValue OldValue { get; }

		public TValue NewValue { get; }
	}
}