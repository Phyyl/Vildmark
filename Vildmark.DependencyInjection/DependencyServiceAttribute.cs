using System;

namespace Vildmark.DependencyInjection
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
