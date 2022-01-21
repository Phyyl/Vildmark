using OpenTK.Mathematics;
using Vildmark.Graphics.Materials;
using Vildmark.Graphics.Textures;

namespace Vildmark.Graphics.Fonts
{
    public record struct BitmapFontMaterial(Texture2D[] Textures, Color4 Color) : ITexturesMaterial, IColorMaterial;
}
