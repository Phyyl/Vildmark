using System;
using System.Collections;
using System.Collections.Generic;

namespace Vildmark.DependencyServices
{
	public interface IDependencyService
	{
		T Get<T>() where T : class;
		object Get(Type type);
		IEnumerable<T> GetAll<T>() where T : class;
		IEnumerable<object> GetAll(Type type);

		object Register(Type type, object value, int priority = 0);
		T Register<T>(T value, int priority = 0) where T : class;
		TInstance Register<T, TInstance>(int priority = 0)
			where T : class
			where TInstance : class, T, new();

		T CreateInstance<T>() where T : class;
		object CreateInstance(Type type);
	}
}