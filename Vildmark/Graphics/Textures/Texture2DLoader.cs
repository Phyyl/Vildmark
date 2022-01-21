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
    [Register(typeof(IResourceLoaderOptions<GLTexture2D, Texture2DMode>))]
    [Register(typeof(IResourceLoader<Texture2D>))]
    [Register(typeof(IResourceLoaderOptions<Texture2D, Texture2DMode>))]
    internal class Texture2DLoader :
        IResourceLoader<GLTexture2D>,
        IResourceLoaderOptions<GLTexture2D, Texture2DMode>,
        IResourceLoader<Texture2D>,
        IResourceLoaderOptions<Texture2D, Texture2DMode>
    {
        public unsafe GLTexture2D Load(Stream stream, Assembly? assembly, string? resourceName, Texture2DMode options)
        {
#pragma warning disable CA1416 // Validate platform compatibility
            Bitmap bitmap = new(stream);
            BitmapData data = bitmap.LockBits(new Rectangle(Point.Empty, bitmap.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            Span<byte> span = new(data.Scan0.ToPointer(), bitmap.Width * 4 * bitmap.Height);
            GLTexture2D texture = new(bitmap.Width, bitmap.Height, span);

            bitmap.UnlockBits(data);
#pragma warning restore CA1416 // Validate platform compatibility

            texture.Configure(options.MagFilter, options.MinFilter, options.WrapSMode, options.WrapTMode);

            return texture;
        }

        public unsafe GLTexture2D Load(Stream stream, Assembly? assembly, string? resourceName)
        {
            return Load(stream, assembly, resourceName, Texture2DMode.Default);
        }

        Texture2D IResourceLoader<Texture2D>.Load(Stream stream, Assembly? assembly, string? resourceName)
        {
            return new Texture2D(Load(stream, assembly, resourceName));
        }

        Texture2D IResourceLoaderOptions<Texture2D, Texture2DMode>.Load(Stream stream, Assembly? assembly, string? resourceName, Texture2DMode options)
        {
            return new Texture2D(Load(stream, assembly, resourceName, options));
        }
    }
}
