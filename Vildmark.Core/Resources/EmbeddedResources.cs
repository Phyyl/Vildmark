using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Vildmark.Resources
{
	public static class EmbeddedResources
	{
		public static string GetString(string name, Assembly assembly = default)
		{
			using Stream stream = GetStream(name, assembly ?? Assembly.GetCallingAssembly());
			using StreamReader reader = new StreamReader(stream);

			return reader.ReadToEnd();
		}

		public static byte[] GetBytes(string name, Assembly assembly = default)
		{
			using Stream stream = GetStream(name, assembly ?? Assembly.GetCallingAssembly());

			byte[] result = new byte[stream.Length];

			stream.Read(result, 0, result.Length);

			return result;
		}

		public static Stream GetStream(string name, Assembly assembly = default)
		{
			return (assembly ?? Assembly.GetCallingAssembly()).GetManifestResourceStream(name);
		}
	}
}
