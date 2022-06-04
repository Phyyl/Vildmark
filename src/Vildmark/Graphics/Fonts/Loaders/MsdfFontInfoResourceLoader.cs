using OpenTK.Mathematics;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Vildmark.Graphics.Fonts.Msdf;
using Vildmark.Resources;

namespace Vildmark.Graphics.Fonts.Loaders;

internal class MsdfFontInfoResourceLoader : IResourceLoader<MsdfFontInfo>
{
    public MsdfFontInfo Load(string name, ResourceLoadContext context)
    {
        JsonFontInfo jsonInfo = context.LoadJson<JsonFontInfo>(name);

        return new MsdfFontInfo(jsonInfo.Atlas.Width, jsonInfo.Atlas.Height, jsonInfo.Metrics.LineHeight, jsonInfo.Atlas.DistanceRange, CreateGlyphs(jsonInfo), CreateKernings(jsonInfo));
    }

    private ReadOnlyDictionary<char, MsdfGlyph> CreateGlyphs(JsonFontInfo jsonInfo)
    {
        static Box2 CreateBounds(JsonBounds? jsonBounds) => jsonBounds is null ? new Box2() : new(jsonBounds.Left, jsonBounds.Top, jsonBounds.Right, jsonBounds.Bottom);

        return new(jsonInfo.Glyphs.ToDictionary(g => (char)g.Unicode, g => new MsdfGlyph((char)g.Unicode, CreateBounds(g.PlaneBounds), CreateBounds(g.AtlasBounds), g.Advance)));
    }

    private ReadOnlyDictionary<(char, char), float> CreateKernings(JsonFontInfo jsonInfo)
    {
        return new(jsonInfo.Kerning.ToDictionary(k => ((char)k.Unicode1, (char)k.Unicode2), k => k.Advance));
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private class JsonFontInfo
    {
        [JsonPropertyName("atlas")]
        public JsonAtlas Atlas { get; set; }
        [JsonPropertyName("metrics")]
        public JsonMetrics Metrics { get; set; }
        [JsonPropertyName("glyphs")]
        public JsonGlyph[] Glyphs { get; set; }
        [JsonPropertyName("kerning")]
        public JsonKerning[] Kerning { get; set; }
    }

    private class JsonAtlas
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

    private class JsonMetrics
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

    private class JsonGlyph
    {
        [JsonPropertyName("unicode")]
        public int Unicode { get; set; }
        [JsonPropertyName("advance")]
        public float Advance { get; set; }
        [JsonPropertyName("planeBounds")]
        public JsonBounds? PlaneBounds { get; set; }
        [JsonPropertyName("atlasBounds")]
        public JsonBounds? AtlasBounds { get; set; }
    }

    private class JsonBounds
    {
        [JsonPropertyName("left")]
        public float Left { get; set; }
        [JsonPropertyName("bottom")]
        public float Bottom { get; set; }
        [JsonPropertyName("right")]
        public float Right { get; set; }
        [JsonPropertyName("top")]
        public float Top { get; set; }
    }

    private class JsonKerning
    {
        [JsonPropertyName("unicode1")]
        public int Unicode1 { get; set; }
        [JsonPropertyName("unicode2")]
        public int Unicode2 { get; set; }
        [JsonPropertyName("advance")]
        public float Advance { get; set; }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
