using OpenTK.Graphics.OpenGL4;

namespace Vildmark.Graphics.Textures
{
    //TODO: Add options for backbuffer, zbuffer, etc
    public record class Texture2DFormat
    {
        public static readonly Texture2DFormat Default = new();
        public static readonly Texture2DFormat Texture2D = Default;
        public static readonly Texture2DFormat Depth = Default with
        {
            PixelFormat = PixelFormat.DepthComponent,
            PixelType = PixelType.Byte,
            PixelInternalFormat = PixelInternalFormat.DepthComponent
        };

        public TextureTarget Target { get; init; } = TextureTarget.Texture2D;
        public PixelFormat PixelFormat { get; init; } = PixelFormat.Bgra;
        public PixelType PixelType { get; init; } = PixelType.UnsignedByte;
        public PixelInternalFormat PixelInternalFormat { get; init; } = PixelInternalFormat.Rgba;
    }
}
