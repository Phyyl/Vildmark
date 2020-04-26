using System.Collections.Generic;
using System.Reflection;

namespace Vildmark.DependencyInjection
{
	public interface IDependencyServiceAssemblyProvider
	{
		IEnumerable<Assembly> GetAssemblies();
	}
}
