using System;
using System.Linq.Expressions;

namespace DioLive.Helpers.Properties
{
    public static class PropertyHelper
    {
        public static bool SetProperty<TProperty>(this object sender, Expression<Func<TProperty>> property, TProperty newValue, Action setter, PropertyChangingEventHandler<TProperty> propertyChanging, PropertyChangedEventHandler<TProperty> propertyChanged)
        {
            PropertyWrapper prop = PropertyWrapper.Wrap(property);

            TProperty oldValue = (TProperty)prop.GetValue();

            if (PropertyChanging(prop.Name, oldValue, newValue, propertyChanging, sender))
            {
                return false;
            }

            if (setter != null)
            {
                setter();
            }
            else
            {
                prop.SetValue(newValue);
            }

            PropertyChanged(prop.Name, oldValue, newValue, propertyChanged, sender);
            return true;
        }

        private static bool PropertyChanging<T>(string propertyName, T oldValue, T newValue, PropertyChangingEventHandler<T> propertyChanging, object sender)
        {
            var eventArgs = new PropertyChangingEventArgs<T>(propertyName, oldValue, newValue);
            propertyChanging?.Invoke(sender, eventArgs);
            return eventArgs.Cancel;
        }

        private static void PropertyChanged<T>(string propertyName, T oldValue, T newValue, PropertyChangedEventHandler<T> propertyChanged, object sender)
        {
            var eventArgs = new PropertyChangedEventArgs<T>(propertyName, oldValue, newValue);
            propertyChanged?.Invoke(sender, eventArgs);
        }
    }
}