using System;
using System.Collections.Generic;

namespace Vildmark.DependencyServices
{
	public interface IDependencyServiceTypeProvider
	{
		IEnumerable<(Type serviceType, Type instanceType)> GetServices();
	}
}
