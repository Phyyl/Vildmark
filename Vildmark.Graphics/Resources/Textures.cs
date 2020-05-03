using System;
using System.Collections.Generic;
using System.Text;
using Vildmark.Graphics.GLObjects;

namespace Vildmark.Graphics.Resources
{
	public static class Textures
	{
		public static GLTexture2D WhitePixel { get; } = GLTexture2D.FromPixels(1, 1, 255, 255, 255, 255);
	}
}
