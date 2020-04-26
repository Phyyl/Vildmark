using System;

namespace Vildmark.DependencyServices
{
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	public class DependencyServicesAssemblyAttribute : Attribute
	{
	}
}
