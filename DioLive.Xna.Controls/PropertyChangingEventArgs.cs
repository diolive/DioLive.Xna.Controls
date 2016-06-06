namespace DioLive.Xna.Controls
{
    public class PropertyChangingEventArgs<TValue> : PropertyChangedEventArgs<TValue>
    {
        public PropertyChangingEventArgs(string propertyName, TValue oldValue, TValue newValue)
            : base(propertyName, oldValue, newValue)
        {
        }

        public bool Cancel { get; set; }
    }
}