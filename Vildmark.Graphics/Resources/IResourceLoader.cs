using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Vildmark.Graphics.Resources
{
	public interface IResourceLoader<T>
	{
		T Load(Stream stream);

	}
}
