using System;

namespace Vildmark.DependencyInjection
{
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	public class DependencyServicesAssemblyAttribute : Attribute
	{
	}
}
