using System.Collections.Generic;
using System.Reflection;

namespace Vildmark.DependencyServices
{
	public interface IDependencyServiceAssemblyProvider
	{
		IEnumerable<Assembly> GetAssemblies();
	}
}
