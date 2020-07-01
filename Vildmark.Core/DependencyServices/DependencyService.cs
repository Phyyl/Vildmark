using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Vildmark.DependencyServices
{
	public class DependencyService : IDependencyService
	{
		public static IDependencyService Global { get; set; } = new AutoRegisterDependencyService(new DependencyService());

		static DependencyService()
		{
			IDependencyServiceAssemblyProvider assemblyProvider = new AppDomainDependencyServiceAssemblyProvider();
			IDependencyServiceTypeProvider provider = new AttributeDependencyServiceTypeProvider(assemblyProvider);
			IDependencyServiceTypeRegistrer registrer = new CascadingDependencyServiceTypeRegistrer(Global, provider);

			registrer.RegisterServices();
		}

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

		public object CreateInstance(Type type)
		{
			object value = CreateInstance(type, false);

			if (value is { })
			{
				return value;
			}

			return CreateInstance(type, true);
		}

		private object CreateInstance(Type type, bool throwOnError)
		{
			foreach (var constructor in type.GetConstructors(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic).OrderBy(c => c.IsPublic ? 0 : 1))
			{
				var parameters = constructor.GetParameters();
				var parameterValues = parameters.Select(p => Get(p.ParameterType)).ToArray();

				if (parameters.Any(p => p is null))
				{
					if (throwOnError)
					{
						string missingServices = string.Join(", ", Enumerable.Range(0, parameters.Length).Where(i => parameterValues[i] is null).Select(i => parameters[i].ParameterType.Name));

						throw new Exception($"Missing services ({missingServices})");
					}
					else
					{
						continue;
					}
				}

				object value = constructor.Invoke(parameterValues);

				if (value is null)
				{
					continue;
				}

				return value;
			}

			return default;
		}
	}
}
