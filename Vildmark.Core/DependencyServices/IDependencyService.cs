using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Vildmark.DependencyServices
{
	public interface IDependencyService
	{
		T Get<T>() where T : class;
		object Get(Type type);

		T Create<T>() where T : class;
		object Create(Type type);
	}
}