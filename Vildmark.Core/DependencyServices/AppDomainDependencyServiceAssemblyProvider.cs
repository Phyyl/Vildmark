using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Vildmark.DependencyServices
{
	public class AppDomainDependencyServiceAssemblyProvider : IDependencyServiceAssemblyProvider
	{
		public IEnumerable<Assembly> GetAssemblies()
		{
            foreach (var assembly in Assembly.GetEntryAssembly().GetReferencedAssemblies())
            {
                try
                {
					var x = Assembly.LoadFrom($"{assembly.Name}.dll");
                }
                catch { }
			}

			return AppDomain.CurrentDomain.GetAssemblies().Where(a => Attribute.IsDefined(a, typeof(DependencyServicesAssemblyAttribute)));
		}
	}
}
