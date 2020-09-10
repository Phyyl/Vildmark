using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vildmark.DependencyServices
{
	public static class Service<T> where T : class
	{
		private static T instance;

		public static T Instance => instance ??= GetInstance();

		private static T GetInstance()
		{
			return DependencyService.Global.Get<T>();
		}
	}
}
