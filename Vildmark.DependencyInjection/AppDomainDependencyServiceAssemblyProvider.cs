using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Vildmark.DependencyInjection
{
	public class AppDomainDependencyServiceAssemblyProvider : IDependencyServiceAssemblyProvider
	{
		public IEnumerable<Assembly> GetAssemblies()
		{
			return AppDomain.CurrentDomain.GetAssemblies().Where(a => Attribute.IsDefined(a, typeof(DependencyServicesAssemblyAttribute)));
		}
	}
}
