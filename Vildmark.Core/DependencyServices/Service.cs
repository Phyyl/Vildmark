using System;
using System.Collections.Generic;
using System.Text;

namespace Vildmark.DependencyServices
{
	public static class Service<T> where T : class
	{
		private static T instance;

		public static T Instance => instance ??= GetInstance();

		private static T GetInstance()
		{
			return Service.DependencyService.Get<T>();
		}
	}

	internal static class Service
	{
		internal static IDependencyService DependencyService { get; } = new AutoRegisterDependencyService(new DependencyService());
	}
}
