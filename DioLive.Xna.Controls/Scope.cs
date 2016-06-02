using System;
using System.Linq.Expressions;
using System.Reflection;

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
			MemberExpression memberExpr = property.Body as MemberExpression;
			if (memberExpr == null)
			{
				throw new ArgumentException("Expression should select object field or property", nameof(property));
			}

			Func<object, object> getValue;
			Action<object, object> setValue;

			PropertyInfo propInfo = memberExpr.Member as PropertyInfo;
			if (propInfo != null)
			{
				getValue = propInfo.GetValue;
				setValue = propInfo.SetValue;
			}
			else
			{
				FieldInfo fieldInfo = memberExpr.Member as FieldInfo;
				if (fieldInfo != null)
				{
					getValue = fieldInfo.GetValue;
					setValue = fieldInfo.SetValue;
				}
				else
				{
					throw new ArgumentException("Expression should select object field or property", nameof(property));
				}
			}

			object target = Expression.Lambda(memberExpr.Expression).Compile().DynamicInvoke();
			object originalValue = getValue(target);
			setValue(target, scopeValue);

			return new Scope(() => setValue(target, originalValue));
		}

		public void Dispose()
		{
			this.restore();
		}
	}
}