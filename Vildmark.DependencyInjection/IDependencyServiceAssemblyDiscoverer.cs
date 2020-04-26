using System.Collections.Generic;
using System.Reflection;

namespace Ashborn.DependencyServices
{
	public interface IDependencyServiceAssemblyProvider
	{
		IEnumerable<Assembly> GetAssemblies();
	}
}
