using OpenTK.Graphics.OpenGL4;

namespace Vildmark.Graphics.Textures;

//TODO: Add options for backbuffer, zbuffer, etc
public record class Texture2DParameters
{
    public static readonly Texture2DParameters Default = new();
    public static readonly Texture2DParameters Texture2D = Default;
    public static readonly Texture2DParameters Depth = Default with
    {
        PixelFormat = PixelFormat.DepthComponent,
        PixelType = PixelType.Byte,
        PixelInternalFormat = PixelInternalFormat.DepthComponent
    };

    public TextureTarget Target { get; init; } = TextureTarget.Texture2D;
    public PixelFormat PixelFormat { get; init; } = PixelFormat.Bgra;
    public PixelType PixelType { get; init; } = PixelType.UnsignedByte;
    public PixelInternalFormat PixelInternalFormat { get; init; } = PixelInternalFormat.Rgba;
    public TextureMagFilter MagFilter { get; init; } = TextureMagFilter.Nearest;
    public TextureMinFilter MinFilter { get; init; } = TextureMinFilter.Nearest;
    public TextureWrapMode WrapS { get; init; } = TextureWrapMode.ClampToBorder;
    public TextureWrapMode WrapT { get; init; } = TextureWrapMode.ClampToBorder;
}
