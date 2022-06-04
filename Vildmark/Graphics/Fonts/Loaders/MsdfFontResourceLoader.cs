using OpenTK.Mathematics;
using System.Text.Json.Serialization;
using Vildmark.Graphics.Fonts.Msdf;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Textures;
using Vildmark.Resources;

namespace Vildmark.Graphics.Fonts.Loaders;

internal class MsdfFontResourceLoader : IResourceLoader<MsdfFont>
{
    public MsdfFont Load(string name, ResourceLoadContext context)
    {
        FontInfo info = context.LoadAsJson<FontInfo>($"{name}.json");
        Texture2D texture = context.Load<Texture2D>($"{name}.png");

        return new MsdfFont(info, texture);
    }

    internal class FontInfo
    {
        [JsonPropertyName("atlas")]
        public Atlas? Atlas { get; set; }
        [JsonPropertyName("metrics")]
        public Metrics? Metrics { get; set; }
        [JsonPropertyName("glyphs")]
        public Glyph[]? Glyphs { get; set; }
        [JsonPropertyName("kerning")]
        public Kerning[]? Kerning { get; set; }
    }

    internal class Atlas
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        [JsonPropertyName("distanceRange")]
        public int DistanceRange { get; set; }
        [JsonPropertyName("size")]
        public int Size { get; set; }
        [JsonPropertyName("width")]
        public int Width { get; set; }
        [JsonPropertyName("height")]
        public int Height { get; set; }
        [JsonPropertyName("yOrigin")]
        public string? YOrigin { get; set; }
    }

    internal class Metrics
    {
        [JsonPropertyName("emSize")]
        public int EmSize { get; set; }
        [JsonPropertyName("lineHeight")]
        public float LineHeight { get; set; }
        [JsonPropertyName("ascender")]
        public float Ascender { get; set; }
        [JsonPropertyName("descender")]
        public float Descender { get; set; }
        [JsonPropertyName("underlineY")]
        public float UnderlineY { get; set; }
        [JsonPropertyName("underlineThickness")]
        public float UnderlineThickness { get; set; }
    }

    internal class Glyph
    {
        [JsonPropertyName("unicode")]
        public int Unicode { get; set; }
        [JsonPropertyName("advance")]
        public float Advance { get; set; }
        [JsonPropertyName("planeBounds")]
        public Bounds? PlaneBounds { get; set; }
        [JsonPropertyName("atlasBounds")]
        public Bounds? AtlasBounds { get; set; }
    }

    internal class Bounds
    {
        [JsonPropertyName("left")]
        public float Left { get; set; }
        [JsonPropertyName("bottom")]
        public float Bottom { get; set; }
        [JsonPropertyName("right")]
        public float Right { get; set; }
        [JsonPropertyName("top")]
        public float Top { get; set; }

        public Vector2 TopLeft => new(Left, Top);
        public Vector2 TopRight => new(Right, Top);
        public Vector2 BottomLeft => new(Left, Bottom);
        public Vector2 BottomRight => new(Right, Bottom);
    }

    internal class Kerning
    {
        [JsonPropertyName("unicode1")]
        public int Unicode1 { get; set; }
        [JsonPropertyName("unicode2")]
        public int Unicode2 { get; set; }
        [JsonPropertyName("advance")]
        public float Advance { get; set; }
    }
}
