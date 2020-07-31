using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Vildmark.DependencyServices;
using Vildmark.Graphics.Resources;

namespace Vildmark.Resources
{
	[DependencyService(typeof(IResourceLoader<Stream, string>))]
	public class StringResourceLoader : IResourceLoader<Stream, string>
	{
		public string Load(Stream stream, Assembly assembly)
		{
			using StreamReader reader = new StreamReader(stream);

			return reader.ReadToEnd();
		}
	}
}
