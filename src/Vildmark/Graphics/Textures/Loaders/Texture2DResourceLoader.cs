using System.Drawing.Imaging;
using System.Drawing;
using Vildmark.Graphics.GLObjects;
using Vildmark.Resources;
using OpenTK.Graphics.OpenGL4;

namespace Vildmark.Graphics.Textures.Loaders;

public record Texture2DOptions(TextureMagFilter MagFilter, TextureMinFilter MinFilter, TextureWrapMode WrapS, TextureWrapMode WrapT)
{
    public static readonly Texture2DOptions Nearest = new(TextureMagFilter.Nearest, TextureMinFilter.Nearest, TextureWrapMode.ClampToBorder, TextureWrapMode.ClampToBorder);
    public static readonly Texture2DOptions Linear = new(TextureMagFilter.Linear, TextureMinFilter.Linear, TextureWrapMode.ClampToBorder, TextureWrapMode.ClampToBorder);
}

internal class Texture2DResourceLoader : IResourceLoader<Texture2D, Texture2DOptions>
{
    public Texture2D Load(string name, ResourceLoadContext context)
    {
        return Load(name, Texture2DOptions.Nearest, context);
    }

    public unsafe Texture2D Load(string name, Texture2DOptions options, ResourceLoadContext context)
    {
        Bitmap bitmap = new(context.GetStream(name));
        BitmapData data = bitmap.LockBits(new Rectangle(Point.Empty, bitmap.Size), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        Span<byte> span = new(data.Scan0.ToPointer(), bitmap.Width * 4 * bitmap.Height);
        GLTexture2D texture = new(bitmap.Width, bitmap.Height, span);
        
        bitmap.UnlockBits(data);
        texture.Configure(options.MagFilter, options.MinFilter, options.WrapS, options.WrapT);

        return new(texture);
    }
}
