using OpenTK.Graphics.OpenGL;

namespace Vildmark.Graphics.GLObjects
{
    public record TextureOptions
    {
        //TODO: Add options for backbuffer, zbuffer, etc
        public static readonly TextureOptions Default = new();
        public static readonly TextureOptions Linear = Default;
        public static readonly TextureOptions Nearest = new()
        {
            MagFilter = TextureMagFilter.Nearest,
            MinFilter = TextureMinFilter.Nearest
        };
        public static readonly TextureOptions Depth = Nearest with
        {
            PixelFormat = PixelFormat.DepthComponent,
            PixelInternalFormat = PixelInternalFormat.DepthComponent,
            WrapSMode = TextureWrapMode.Repeat,
            WrapTMode = TextureWrapMode.Repeat
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
