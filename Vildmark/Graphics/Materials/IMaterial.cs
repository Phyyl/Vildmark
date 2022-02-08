using OpenTK.Mathematics;
using Vildmark.Graphics.Textures;

namespace Vildmark.Graphics.Materials
{
    public interface ITexturesMaterial
    {
        Texture2D[] Textures { get; }
    }

    public interface IColorMaterial
    {
        Color4 Color { get; }
    }

    public interface ITextureMaterial
    {
        Texture2D Texture { get; }
    }

    public record class ColorMaterial(Color4 Color) : IColorMaterial;
    public record class TextureMaterial(Texture2D Texture) : ITextureMaterial;
    public record class TintedTextureMaterial(Texture2D Texture, Color4 Color) : ITextureMaterial, IColorMaterial;
}
