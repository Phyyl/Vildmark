using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Vildmark.Resources
{
	public interface IEmbeddedResourceLoader<T>
	{
		T Load(string name, Assembly assembly = default);
	}
}
