using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Vildmark.Graphics.Resources
{
	public interface IResourceLoader<TParameter, TResult>
	{
		TResult Load(TParameter parameter, Assembly assembly);
	}
}
