using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.Rendering;

namespace Vildmark.Graphics.Materials
{
    public interface IMaterial
    {
    }

    public interface ITexturesMaterial : IMaterial
    {
        Texture2D[] Textures { get; }
    }

    public interface IColorMaterial : IMaterial
    {
        Color4 Color { get; }
    }

    public interface ITextureMaterial : IMaterial
    {
        Texture2D Texture { get; }
    }

    public record class ColorMaterial(Color4 Color) : IColorMaterial;
    public record class TextureMaterial(Texture2D Texture) : ITextureMaterial;
    public record class TintedTextureMaterial(Texture2D Texture, Color4 Color) : ITextureMaterial, IColorMaterial;
}