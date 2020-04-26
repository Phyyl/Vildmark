using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Vildmark.DependencyInjection
{
	public class AttributeDependencyServiceTypeProvider : IDependencyServiceTypeProvider
	{
		private readonly IEnumerable<Assembly> assemblies;

		public AttributeDependencyServiceTypeProvider(IEnumerable<Assembly> assemblies)
		{
			this.assemblies = assemblies;
		}

		public IEnumerable<(Type serviceType, Type instanceType)> GetServices()
		{
			foreach (var type in assemblies.SelectMany(a => a.GetTypes()))
			{
				DependencyServiceAttribute dependencyAttribute = type.GetCustomAttribute<DependencyServiceAttribute>();

				if (dependencyAttribute?.ServiceType is null || type.IsAbstract)
				{
					continue;
				}

				yield return (dependencyAttribute?.ServiceType, type);
			}
		}
	}
}
