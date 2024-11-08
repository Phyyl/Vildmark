using Vildmark.Graphics.GLObjects;
using Vildmark.Resources;
using OpenTK.Graphics.OpenGL;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Buffers;

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
        Image baseImage = Image.Load(context.GetStream(name));
        using var image = baseImage.CloneAs<Bgra32>();
        using var buffer = MemoryPool<byte>.Shared.Rent(image.Width * image.Height * 4);

        image.CopyPixelDataTo(buffer.Memory.Span);

        GLTexture2D texture = new(image.Width, image.Height, buffer.Memory.Span, new()
        {
            MagFilter = options.MagFilter,
            MinFilter = options.MinFilter,
            WrapS = options.WrapS,
            WrapT = options.WrapT
        });

        return new(texture);
    }
}
