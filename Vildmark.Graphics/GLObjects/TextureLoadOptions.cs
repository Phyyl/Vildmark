using OpenTK.Graphics.OpenGL;

namespace Vildmark.Graphics.GLObjects
{
    public class TextureLoadOptions
    {
        //TODO: Add options for backbuffer, zbuffer, etc
        public static readonly TextureLoadOptions Default = new();
        public static readonly TextureLoadOptions Linear = Default;
        public static readonly TextureLoadOptions Nearest = new()
        {
            MagFilter = TextureMagFilter.Nearest,
            MinFilter = TextureMinFilter.Nearest
        };

        public TextureMagFilter MagFilter { get; init; } = TextureMagFilter.Linear;
        public TextureMinFilter MinFilter { get; init; } = TextureMinFilter.Linear;
        public TextureWrapMode WrapSMode { get; init; } = TextureWrapMode.ClampToEdge;
        public TextureWrapMode WrapTMode { get; init; } = TextureWrapMode.ClampToEdge;
        public TextureTarget Target { get; init; } = TextureTarget.Texture2D;
        public PixelFormat PixelFormat { get; init; } = PixelFormat.Bgra;
        public PixelType PixelType { get; init; } = PixelType.UnsignedByte;
        public PixelInternalFormat PixelInternalFormat { get; init; } = PixelInternalFormat.Rgba;
    }
}
