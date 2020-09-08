using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vildmark.DependencyServices
{
	public static class Service<T> where T : class
	{
		private static T[] instances;

		public static T Instance => Instances.FirstOrDefault();

		public static IEnumerable<T> Instances => instances ??= GetInstances();

		private static T[] GetInstances()
		{
			return Service.DependencyService.GetAll<T>().ToArray();
		}
	}

	internal static class Service
	{
		internal static IDependencyService DependencyService { get; } = new AppDomainDependencyService();
	}
}
