namespace DioLive.Xna.Controls.Helpers
{
	using System;
	using System.Reflection;

	public sealed class SingletonCreator<S>
			where S : class
	{
		public static S CreatorInstance
		{
			get { return instance; }
		}

		//Используется Reflection для создания экземпляра класса без публичного конструктора
		private static readonly S instance = (S)typeof(S).GetConstructor(
							    BindingFlags.Instance | BindingFlags.NonPublic,
							    null,
							    new Type[0],
							    new ParameterModifier[0]).Invoke(null);
	}
}