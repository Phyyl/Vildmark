using System;
using System.Collections.Generic;
using System.Text;

namespace Vildmark.DependencyServices
{
	public class AutoRegisterDependencyService : IDependencyService
	{
		private readonly IDependencyService dependencyService;

		public AutoRegisterDependencyService(IDependencyService dependencyService)
		{
			this.dependencyService = dependencyService;

			RegisterServices();
		}

		public T CreateInstance<T>() where T : class
		{
			return ((IDependencyService)dependencyService).CreateInstance<T>();
		}

		public object CreateInstance(Type type)
		{
			return ((IDependencyService)dependencyService).CreateInstance(type);
		}

		public T Get<T>() where T : class
		{
			return ((IDependencyService)dependencyService).Get<T>();
		}

		public object Get(Type type)
		{
			return ((IDependencyService)dependencyService).Get(type);
		}

		public object Register(Type type, object value)
		{
			return ((IDependencyService)dependencyService).Register(type, value);
		}

		public T Register<T>(T value) where T : class
		{
			return ((IDependencyService)dependencyService).Register(value);
		}

		TInstance IDependencyService.Register<T, TInstance>()
		{
			return ((IDependencyService)dependencyService).Register<T, TInstance>();
		}

		private void RegisterServices()
		{
			CascadingDependencyServiceTypeRegistrer registrer = new CascadingDependencyServiceTypeRegistrer(dependencyService, new AttributeDependencyServiceTypeProvider(new AppDomainDependencyServiceAssemblyProvider()));

			registrer.RegisterServices();
		}
	}
}
