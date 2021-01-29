using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Text;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Rendering;
using Vildmark.Resources;

namespace Vildmark.Graphics.Resources
{
    [Register(typeof(IResourceLoader<GLTexture2D>))]
    [Register(typeof(IResourceLoader<Texture2D>))]
    [Register(typeof(TextureResourceLoader))]
    public class TextureResourceLoader : IResourceLoader<GLTexture2D>, IResourceLoader<Texture2D>
    {
        public TextureMagFilter TextureMagFilter { get; } = TextureMagFilter.Nearest;
        public TextureMinFilter TextureMinFilter { get; } = TextureMinFilter.Nearest;
        public TextureWrapMode WrapSMode { get; } = TextureWrapMode.ClampToEdge;
        public TextureWrapMode WrapTMode { get; } = TextureWrapMode.ClampToEdge;

        Texture2D IResourceLoader<Texture2D>.Load(Stream stream)
        {
            return new Texture2D(ResourceLoader.Load<GLTexture2D>(stream));
        }

        unsafe GLTexture2D IResourceLoader<GLTexture2D>.Load(Stream stream)
        {
            Bitmap bitmap = new Bitmap(stream);

            BitmapData data = bitmap.LockBits(new Rectangle(Point.Empty, bitmap.Size), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GLTexture2D texture = new GLTexture2D(bitmap.Width, bitmap.Height, new Span<byte>(data.Scan0.ToPointer(), bitmap.Width * 4 * bitmap.Height), TextureMagFilter, TextureMinFilter, WrapSMode, WrapTMode);

            bitmap.UnlockBits(data);

            return texture;
        }
    }
}
