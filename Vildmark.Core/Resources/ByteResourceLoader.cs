using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Vildmark.DependencyServices;
using Vildmark.Graphics.Resources;

namespace Vildmark.Resources
{
	[DependencyService(typeof(ByteResourceLoader))]
	[DependencyService(typeof(IResourceLoader<byte[]>))]
	public class ByteResourceLoader : IResourceLoader<byte[]>
	{
		public byte[] Load(Stream stream)
		{
			using MemoryStream memoryStream = new MemoryStream();

			stream.CopyTo(memoryStream);

			return memoryStream.ToArray();
		}
	}
}
