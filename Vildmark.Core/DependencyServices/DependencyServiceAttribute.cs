using System;

namespace Vildmark.DependencyServices
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class DependencyServiceAttribute : Attribute
	{
		public DependencyServiceAttribute(Type serviceType, int priority = 0)
		{
			ServiceType = serviceType;
			Priority = priority;
		}

		public Type ServiceType { get; }

		public int Priority { get; }
	}
}
