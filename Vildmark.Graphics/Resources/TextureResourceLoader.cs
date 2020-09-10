using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Text;
using Vildmark.DependencyServices;
using Vildmark.Graphics.GLObjects;

namespace Vildmark.Graphics.Resources
{
	public class TextureResourceLoader : IResourceLoader<Stream, GLTexture2D>
	{
		public unsafe GLTexture2D Load(Stream stream, Assembly assembly)
		{
			Bitmap bitmap = new Bitmap(stream);

			BitmapData data = bitmap.LockBits(new Rectangle(Point.Empty, bitmap.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

			GLTexture2D texture = new GLTexture2D(bitmap.Width, bitmap.Height, new Span<byte>(data.Scan0.ToPointer(), bitmap.Width * 4 * bitmap.Height));

			bitmap.UnlockBits(data);

			return texture;
		}
	}
}
