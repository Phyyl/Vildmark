using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
            List<Type> services = provider.GetServiceTypes().ToList();

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
                    Type instanceType = services[i];
                    object instance = dependencyService.CreateInstance(instanceType);

                    if (instance is null)
                    {
                        continue;
                    }

                    foreach (var attribute in instanceType.GetCustomAttributes<DependencyServiceAttribute>())
                    {
                        dependencyService.Register(attribute.ServiceType, instance);
                    }

                    if (dependencyService.Get(instance.GetType()) is null)
                    {
                        dependencyService.Register(instance.GetType(), instance);
                    }

                    services.RemoveAt(i);
                }

            }
            while (services.Count < previousCount);

            foreach (var service in services)
            {
                Console.WriteLine($"Failed to initialize service {service.Name}");
            }
        }
    }
}
