using System;

namespace Vildmark.DependencyInjection
{
	public interface IDependencyService
	{
		T Get<T>() where T : class;
		object Get(Type type);

		object Register(Type type, object value);
		T Register<T>(T value) where T : class;
		TInstance Register<T, TInstance>()
			where T : class
			where TInstance : class, T, new();

		T CreateInstance<T>() where T : class;
		object CreateInstance(Type type);
	}
}