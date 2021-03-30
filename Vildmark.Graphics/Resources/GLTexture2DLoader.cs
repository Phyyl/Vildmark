using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using Vildmark.Graphics.GLObjects;
using Vildmark.Resources;

namespace Vildmark.Graphics.Resources
{
    [Register(typeof(IResourceLoader<GLTexture2D>))]
    internal class GLTexture2DLoader : IResourceLoader<GLTexture2D>, IResourceLoaderOptions<GLTexture2D, TextureLoadOptions>
    {
        private TextureLoadOptions options;

        public TextureLoadOptions Options
        {
            get => options;
            set => options = value ?? TextureLoadOptions.Default;
        }

        public unsafe GLTexture2D Load(Stream stream, Assembly assembly, string resourceName)
        {
            Bitmap bitmap = new(stream);
            BitmapData data = bitmap.LockBits(new Rectangle(Point.Empty, bitmap.Size), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Span<byte> span = new(data.Scan0.ToPointer(), bitmap.Width * 4 * bitmap.Height);
            GLTexture2D texture = new(bitmap.Width, bitmap.Height, span, Options);

            bitmap.UnlockBits(data);

            return texture;
        }
    }
}
