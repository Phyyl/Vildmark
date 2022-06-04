using OpenTK.Mathematics;
using System.Drawing;
using Vildmark.Graphics.Fonts.Loaders;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Meshes;
using Vildmark.Graphics.Rendering;
using Vildmark.Resources;
using static Vildmark.Graphics.Fonts.Loaders.MsdfFontResourceLoader;

namespace Vildmark.Graphics.Fonts.Msdf;

public class MsdfFont : IResource<MsdfFont>
{
    private readonly MsdfText text;

    public static IResourceLoader<MsdfFont> Loader => new MsdfFontResourceLoader();

    internal static MsdfShader Shader { get; } = new();

    internal GLTexture2D Texture { get; }
    internal FontInfo Info { get; }

    internal MsdfFont(FontInfo info, GLTexture2D texture)
    {
        Info = info;
        Texture = texture;

        text = new(this, "", 64);
    }

    public void RenderString(Renderer renderer, string text, float size, Color4 foreground, Color4? background = default, Transform? transform = default)
    {
        this.text.Text = text;
        this.text.FontSize = size;

        this.text.Render(renderer, foreground, background, transform);
    }

    public Vertex[] CreateMesh(string text, float size)
    {
        if (Info.Metrics is null || Info.Atlas is null || Info.Glyphs is null)
        {
            throw new Exception("Invalid font descriptor");
        }

        List<Vertex> vertices = new();
        Vector2 position = new(0, Info.Metrics.LineHeight * size);
        foreach (var chr in text)
        {
            if (Info.Glyphs.FirstOrDefault(g => g.Unicode == chr) is not Glyph glyph)
            {
                continue;
            }

            if (glyph.PlaneBounds is not null && glyph.AtlasBounds is not null)
            {
                Vector2 tl = glyph.PlaneBounds.TopLeft * size;
                Vector2 bl = glyph.PlaneBounds.BottomLeft * size;
                Vector2 br = glyph.PlaneBounds.BottomRight * size;
                Vector2 tr = glyph.PlaneBounds.TopRight * size;

                Vector2 ttl = glyph.AtlasBounds.TopLeft;
                Vector2 tbl = glyph.AtlasBounds.BottomLeft;
                Vector2 tbr = glyph.AtlasBounds.BottomRight;
                Vector2 ttr = glyph.AtlasBounds.TopRight;

                vertices.Add(new(position + tl, ttl / Texture.Size));
                vertices.Add(new(position + bl, tbl / Texture.Size));
                vertices.Add(new(position + br, tbr / Texture.Size));
                vertices.Add(new(position + tl, ttl / Texture.Size));
                vertices.Add(new(position + br, tbr / Texture.Size));
                vertices.Add(new(position + tr, ttr / Texture.Size));
            }

            position += new Vector2(glyph.Advance * size, 0);
        }

        return vertices.ToArray();
    }

    public RectangleF MeasureString(string text, float size)
    {
        Vertex[] vertices = CreateMesh(text, size);

        Vector2 min = new(vertices.Min(v => v.Position.X), vertices.Min(v => v.Position.Y));
        Vector2 max = new(vertices.Max(v => v.Position.X), vertices.Max(v => v.Position.Y));

        return new RectangleF(min.X, min.Y, max.X - min.X, max.Y - min.Y);
    }

    public MsdfText CreateText(string text, float size)
    {
        return new MsdfText(this, text, size);
    }
}
