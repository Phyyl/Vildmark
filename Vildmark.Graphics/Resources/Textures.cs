using System;
using System.Collections.Generic;
using System.Text;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Rendering;

namespace Vildmark.Graphics.Resources
{
	public static class Textures
	{
		public static Texture2D WhitePixel { get; } = GLTexture2D.FromPixels(1, 1, 255, 255, 255, 255);
	}
}
