using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Vildmark.Helpers;

namespace Vildmark.DependencyServices
{
    public class AppDomainDependencyService : DependencyService
    {
        public AppDomainDependencyService()
        {
            RegisterAll();
        }

        private void RegisterAll()
        {
            AssemblyHelper.LoadAllReferencedAssemblies();

            Assembly[] assemblies = AssemblyHelper.GetAllLoadedUserAssemblies().ToArray();

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

                    Register(serviceType, CreateInstance(instanceType), dependencyAttribute.Priority);
                }
            }
        }

		//return AppDomain.CurrentDomain.GetAssemblies().Where(a => Attribute.IsDefined(a, typeof(DependencyServicesAssemblyAttribute)));
	}
}
