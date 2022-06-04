using OpenTK.Mathematics;
using System.Collections.ObjectModel;
using Vildmark.Graphics.Fonts.Loaders;
using Vildmark.Resources;

namespace Vildmark.Graphics.Fonts.Msdf;

[ResourceLoader(typeof(MsdfFontInfoResourceLoader))]
internal record class MsdfFontInfo(
    float Width, 
    float Height, 
    float LineHeight, 
    float DistanceRange, 
    ReadOnlyDictionary<char, MsdfGlyph> Glyphs,
    ReadOnlyDictionary<(char, char), float> Kernings)
{
    public Vector2 Size => new(Width, Height);

    public bool TryGetKerning(char left, char right, out float advance)
    {
        return Kernings.TryGetValue((left, right), out advance);
    }

    public bool TryGetGlyph(char chr, out MsdfGlyph glyph)
    {
        return Glyphs.TryGetValue(chr, out glyph);
    }
}
