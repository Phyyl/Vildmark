using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Vildmark.Graphics.Resources;

namespace Vildmark.Resources
{
	public class EmbeddedResourceLoader<T> : IEmbeddedResourceLoader<T>
	{
		private readonly IResourceLoader<T> resourceLoader;

		public EmbeddedResourceLoader(IResourceLoader<T> resourceLoader)
		{
			this.resourceLoader = resourceLoader;
		}

		public T Load(string name, Assembly assembly = null)
		{
			return resourceLoader.Load(GetStream(name, assembly ?? Assembly.GetCallingAssembly()));
		}

		private Stream GetStream(string name, Assembly assembly)
		{
			return assembly?.GetManifestResourceStream(name);
		}
	}
}
