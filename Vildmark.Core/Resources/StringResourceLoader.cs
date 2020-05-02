using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Vildmark.DependencyServices;
using Vildmark.Graphics.Resources;

namespace Vildmark.Resources
{
	[DependencyService(typeof(IResourceLoader<string>))]
	[DependencyService(typeof(StringResourceLoader))]
	public class StringResourceLoader : IResourceLoader<string>
	{
		public string Load(Stream stream)
		{
			using StreamReader reader = new StreamReader(stream);

			return reader.ReadToEnd();
		}
	}
}
