using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Vildmark.Helpers;

namespace Vildmark.DependencyServices
{
	public class AutoRegisterDependencyService : DependencyService
	{
		static AutoRegisterDependencyService()
        {
			AssemblyHelper.LoadAllReferencedAssemblies();
        }

		public AutoRegisterDependencyService()
		{
			RegisterServices();
		}

		private void RegisterServices()
		{
			CascadingDependencyServiceTypeRegistrer registrer = new CascadingDependencyServiceTypeRegistrer(this, new AttributeDependencyServiceTypeProvider(new AppDomainDependencyService()));

			registrer.RegisterServices();
		}
	}
}
