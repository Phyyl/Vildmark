using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Vildmark.DependencyServices;
using Vildmark.Graphics.Resources;

namespace Vildmark.Resources
{
	[Service]
	public class ByteResourceLoader : IResourceLoader<Stream, byte[]>
	{
		public byte[] Load(Stream stream, Assembly assembly)
		{
			using MemoryStream memoryStream = new MemoryStream();

			stream.CopyTo(memoryStream);

			return memoryStream.ToArray();
		}
	}
}
