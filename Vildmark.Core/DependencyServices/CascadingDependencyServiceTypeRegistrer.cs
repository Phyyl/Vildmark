using System;
using System.Collections.Generic;
using System.Linq;

namespace Vildmark.DependencyServices
{
	public class CascadingDependencyServiceTypeRegistrer : IDependencyServiceTypeRegistrer
	{
		private readonly IDependencyService dependencyService;
		private readonly IDependencyServiceTypeProvider provider;

		public CascadingDependencyServiceTypeRegistrer(IDependencyService dependencyService, IDependencyServiceTypeProvider provider)
		{
			this.dependencyService = dependencyService;
			this.provider = provider;
		}

		//TODO: Improve readability and maintainability
		public void RegisterServices()
		{
			List<(Type serviceType, Type instanceType)> services = provider.GetServices().ToList();

			if (services.Count == 0)
			{
				return;
			}

			int previousCount;

			do
			{
				previousCount = services.Count;

				for (int i = services.Count - 1; i >= 0; i--)
				{
					(Type serviceType, Type instanceType) = services[i];

					object instance = dependencyService.CreateInstance(instanceType);

					if (instance is null)
					{
						continue;
					}

					dependencyService.Register(serviceType, instance);

					services.RemoveAt(i);
				}
			}
			while (services.Count < previousCount);
		}
	}
}
