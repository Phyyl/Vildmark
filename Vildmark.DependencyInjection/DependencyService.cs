using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Vildmark.Common;

namespace Ashborn.DependencyServices
{
	public class DependencyService : IDependencyService
	{
		private readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

		public DependencyService()
		{
			Register<IDependencyService>(this);
		}

		public T Get<T>() where T : class
		{
			return Get(typeof(T)) as T;
		}

		public object Get(Type type)
		{
			return services.GetValueOrDefault(type);
		}

		public object Register(Type type, object value)
		{
			services.AddOrSet(type, value);

			return value;
		}

		public T Register<T>(T value) where T : class
		{
			return Register(typeof(T), value) as T;
		}

		public TInstance Register<T, TInstance>()
			where T : class
			where TInstance : class, T, new()
		{
			return Register<T>(new TInstance()) as TInstance;
		}

		public T CreateInstance<T>() where T : class
		{
			return CreateInstance(typeof(T)) as T;
		}

		//TODO: Prioritize public constructors
		public object CreateInstance(Type type)
		{
			foreach (var constructor in type.GetConstructors(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic))
			{
				try
				{
					var parameters = constructor.GetParameters().Select(p => Get(p.ParameterType)).ToArray();

					if (parameters.Any(p => p is null))
					{
						throw new Exception("Failed to resolve some services");
					}

					object value = constructor.Invoke(parameters);

					if (value is null)
					{
						continue;
					}

					return value;
				}
				catch
				{
				}
			}

			return default;
		}
	}
}
