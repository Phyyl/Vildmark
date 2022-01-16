using OpenTK.Mathematics;
using Vildmark.Graphics.Materials;
using Vildmark.Graphics.Rendering;

namespace Vildmark.Graphics.Fonts
{
    public record struct BitmapFontMaterial(Texture2D[] Textures, Color4 Color) : ITexturesMaterial, IColorMaterial;
}
