using System;
using System.Reflection;

namespace DioLive.Xna.Controls.Helpers
{
    public static class SingletonCreator<T>
            where T : class
    {
        //Используется Reflection для создания экземпляра класса без публичного конструктора
        private static readonly T instance = (T)typeof(T).GetConstructor(
                                BindingFlags.Instance | BindingFlags.NonPublic,
                                null,
                                new Type[0],
                                new ParameterModifier[0]).Invoke(null);

        public static T CreatorInstance
        {
            get { return instance; }
        }
    }
}