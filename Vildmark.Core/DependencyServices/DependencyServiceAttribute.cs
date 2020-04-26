using System;

namespace Vildmark.DependencyServices
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class DependencyServiceAttribute : Attribute
	{
		public DependencyServiceAttribute(Type serviceType)
		{
			ServiceType = serviceType;
		}

		public Type ServiceType { get; }
	}
}
