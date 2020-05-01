using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Vildmark.DependencyServices
{
	public class AttributeDependencyServiceTypeProvider : IDependencyServiceTypeProvider
	{
        private readonly IDependencyServiceAssemblyProvider assemblyProvider;

        public AttributeDependencyServiceTypeProvider(IDependencyServiceAssemblyProvider assemblyProvider)
        {
            this.assemblyProvider = assemblyProvider;
        }

        public IEnumerable<Type> GetServiceTypes()
        {
            Assembly[] assemblies = assemblyProvider.GetAssemblies().ToArray();

            Dictionary<Type, Type> types = new Dictionary<Type, Type>();

            foreach (var instanceType in assemblies.SelectMany(a => a.GetTypes()))
            {
                IEnumerable<DependencyServiceAttribute> dependencyAttributes = instanceType.GetCustomAttributes<DependencyServiceAttribute>();

                foreach (var dependencyAttribute in dependencyAttributes)
                {
                    Type serviceType = dependencyAttribute?.ServiceType;

                    if (serviceType is null || instanceType.IsAbstract)
                    {
                        continue;
                    }

                    if (types.TryGetValue(serviceType, out Type competingType))
                    {
                        DependencyServiceAttribute competingAttribute = competingType.GetCustomAttributes<DependencyServiceAttribute>().First(a => a.ServiceType == serviceType);

                        if (competingAttribute.Priority > dependencyAttribute.Priority)
                        {
                            continue;
                        }
                    }

                    types.Add(serviceType, instanceType);
                }
            }

            return types.Values.Distinct().ToArray();
        }
    }
}
