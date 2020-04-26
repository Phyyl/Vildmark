using System;
using System.Collections.Generic;

namespace Ashborn.DependencyServices
{
	public interface IDependencyServiceTypeProvider
	{
		IEnumerable<(Type serviceType, Type instanceType)> GetServices();
	}
}
