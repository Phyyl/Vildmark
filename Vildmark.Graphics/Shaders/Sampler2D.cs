using Vildmark.Graphics.GLObjects;

namespace Vildmark.Graphics.Shaders
{
    public record Sampler2D(GLTexture2D Texture, int Index = 0)
    {
        public static implicit operator Sampler2D(GLTexture2D texture) => new(texture, 0);
    }
}
