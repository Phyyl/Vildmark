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
			return Service<IResourceLoader<T>>.Instance.Load(GetStream(name, assembly ?? Assembly.GetCallingAssembly()));
		}

		public static Stream GetStream(string name, Assembly assembly = default)
		{
			if (name is null)
			{
				throw new ArgumentNullException(nameof(name));
			}

			assembly ??= Assembly.GetCallingAssembly();

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
