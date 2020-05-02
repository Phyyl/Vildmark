using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Vildmark.Graphics.Resources
{
	public interface IResourceLoader<TParameter, TResult>
	{
		TResult Load(TParameter parameter);
	}

	public interface IResourceLoader<TResult> :IResourceLoader<Stream, TResult>
	{
	}
}
