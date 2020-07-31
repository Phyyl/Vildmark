using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Vildmark.DependencyServices;
using Vildmark.Graphics.Resources;

namespace Vildmark.Resources
{
	public static class EmbeddedResources
	{
		public static T Get<T>(string name, Assembly assembly = default)
		{
			assembly ??= Assembly.GetCallingAssembly();

			IResourceLoader<string, T> stringResourceLoader = Service<IResourceLoader<string, T>>.Instance;

			if (stringResourceLoader != null)
			{
				return stringResourceLoader.Load(name, assembly);
			}

			IResourceLoader<Stream, T> streamResourceLoader = Service<IResourceLoader<Stream, T>>.Instance;

			if (streamResourceLoader != null)
			{
				Stream stream = GetStream(name, assembly);

				if (stream != null)
				{
					return Service<IResourceLoader<Stream, T>>.Instance.Load(stream, assembly);
				}
			}

			return default;
		}

		public static Stream GetStream(string name, Assembly assembly = default)
		{
			assembly ??= Assembly.GetCallingAssembly();

			if (name is null)
			{
				throw new ArgumentNullException(nameof(name));
			}

			string[] names = assembly.GetManifestResourceNames();

			if (!names.Contains(name))
			{
				string closeName = names.FirstOrDefault(n => string.Equals(name, n, StringComparison.InvariantCultureIgnoreCase));

				if (closeName is null)
				{
					closeName = names.FirstOrDefault(n => n.EndsWith(name, StringComparison.InvariantCultureIgnoreCase));
				}

				name = closeName;
			}

			if (name is { })
			{
				return assembly.GetManifestResourceStream(name);
			}

			return null;
		}
	}
}
