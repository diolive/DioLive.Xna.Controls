using System;
using System.Linq.Expressions;

using DioLive.Helpers.Properties;

namespace DioLive.Xna.Controls
{
    public class Scope : IDisposable
    {
        private readonly Action restore;

        private Scope(Action restore)
        {
            this.restore = restore;
        }

        public static Scope UseValue<TProperty>(Expression<Func<TProperty>> property, TProperty scopeValue)
        {
            PropertyWrapper prop = PropertyWrapper.Wrap(property);

            object originalValue = prop.GetValue();
            prop.SetValue(scopeValue);

            return new Scope(() => prop.SetValue(originalValue));
        }

        public void Dispose()
        {
            this.restore();
        }
    }
}