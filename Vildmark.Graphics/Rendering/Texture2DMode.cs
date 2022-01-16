using OpenTK.Graphics.OpenGL4;

namespace Vildmark.Graphics.Rendering
{
    public record Texture2DMode
    {
        public static readonly Texture2DMode Linear = new();
        public static readonly Texture2DMode Nearest = new()
        {
            MagFilter = TextureMagFilter.Nearest,
            MinFilter = TextureMinFilter.Nearest
        };
        public static readonly Texture2DMode Default = Nearest;

        public TextureMagFilter MagFilter { get; init; } = TextureMagFilter.Linear;
        public TextureMinFilter MinFilter { get; init; } = TextureMinFilter.Linear;
        public TextureWrapMode WrapSMode { get; init; } = TextureWrapMode.ClampToEdge;
        public TextureWrapMode WrapTMode { get; init; } = TextureWrapMode.ClampToEdge;
    }
}
