using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Rendering;
using Vildmark.Resources;

namespace Vildmark.Graphics.Resources
{
    [Register(typeof(IResourceLoader<GLTexture2D>))]
    [Register(typeof(IResourceLoaderOptions<GLTexture2D, TextureOptions>))]
    [Register(typeof(IResourceLoader<Texture2D>))]
    [Register(typeof(IResourceLoaderOptions<Texture2D, TextureOptions>))]
    internal class GLTexture2DLoader :
        IResourceLoader<GLTexture2D>,
        IResourceLoaderOptions<GLTexture2D, TextureOptions>,
        IResourceLoader<Texture2D>,
        IResourceLoaderOptions<Texture2D, TextureOptions>
    {
        private TextureOptions? options;

        public TextureOptions Options
        {
            get => options ??= TextureOptions.Default;
            set => options = value;
        }

        public unsafe GLTexture2D Load(Stream stream, Assembly? assembly, string resourceName)
        {
            Bitmap bitmap = new(stream);
            BitmapData data = bitmap.LockBits(new Rectangle(Point.Empty, bitmap.Size), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Span<byte> span = new(data.Scan0.ToPointer(), bitmap.Width * 4 * bitmap.Height);
            GLTexture2D texture = new(bitmap.Width, bitmap.Height, span, Options);

            bitmap.UnlockBits(data);

            return texture;
        }

        Texture2D IResourceLoader<Texture2D>.Load(Stream stream, Assembly? assembly, string resourceName)
        {
            return new Texture2D(Load(stream, assembly, resourceName));
        }
    }
}
