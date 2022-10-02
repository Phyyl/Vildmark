using Vildmark.Graphics.Textures;

namespace Vildmark.Graphics.Shaders;

public record TexturedMaterial(Texture2D Texture, Color4 Tint)
{
    public static implicit operator TexturedMaterial(Color4 tint) => new(Texture2D.WhitePixel, tint);
    public static implicit operator TexturedMaterial(Texture2D texture) => new(texture, Color4.White);
}
