using System;
using System.Collections.Generic;

namespace Vildmark.DependencyInjection
{
	public interface IDependencyServiceTypeProvider
	{
		IEnumerable<(Type serviceType, Type instanceType)> GetServices();
	}
}
