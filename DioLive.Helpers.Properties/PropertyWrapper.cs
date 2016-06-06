using System;
using System.Linq.Expressions;
using System.Reflection;

namespace DioLive.Helpers.Properties
{
    public class PropertyWrapper
    {
        private PropertyWrapper(string name, Func<object, object> getter, Action<object, object> setter, object target)
        {
            this.Name = name;
            this.Getter = getter;
            this.Setter = setter;
            this.Target = target;
        }

        public static PropertyWrapper Wrap<T>(Expression<Func<T>> property)
        {
            MemberExpression memberExpr = property.Body as MemberExpression;
            if (memberExpr == null)
            {
                throw new ArgumentException("Expression should select object field or property", nameof(property));
            }

            Func<object, object> getter;
            Action<object, object> setter;
            string name;

            PropertyInfo propInfo = memberExpr.Member as PropertyInfo;
            if (propInfo != null)
            {
                getter = propInfo.GetValue;
                setter = propInfo.SetValue;
                name = propInfo.Name;
            }
            else
            {
                FieldInfo fieldInfo = memberExpr.Member as FieldInfo;
                if (fieldInfo != null)
                {
                    getter = fieldInfo.GetValue;
                    setter = fieldInfo.SetValue;
                    name = fieldInfo.Name;
                }
                else
                {
                    throw new ArgumentException("Expression should select object field or property", nameof(property));
                }
            }

            object target = Expression.Lambda(memberExpr.Expression).Compile().DynamicInvoke();

            return new PropertyWrapper(name, getter, setter, target);
        }

        public string Name { get; }

        public Func<object, object> Getter { get; }

        public Action<object, object> Setter { get; }

        public object Target { get; }

        public object GetValue() => this.Getter(this.Target);

        public void SetValue(object value) => this.Setter(this.Target, value);
    }
}