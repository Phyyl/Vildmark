﻿using OpenTK.Mathematics;
using Vildmark.Graphics.Fonts.Loaders;
using Vildmark.Graphics.Rendering;
using Vildmark.Graphics.Textures;
using Vildmark.Resources;

namespace Vildmark.Graphics.Fonts.Msdf;

[ResourceLoader(typeof(MsdfFontResourceLoader))]
public class MsdfFont
{
    private readonly MsdfText text;

    internal static MsdfShader Shader { get; } = new();

    internal Texture2D Texture { get; }
    internal MsdfFontInfo Info { get; }

    internal MsdfFont(MsdfFontInfo info, Texture2D texture)
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

    public Vertex[] CreateMesh(string text, float size, float maxLineLength = float.PositiveInfinity)
    {
        if (text.Length == 0 || size <= 0)
        {
            return Array.Empty<Vertex>();
        }

        List<Vertex> vertices = new();
        Vector2 lineStart = new(0, Info.LineHeight * size);
        Vector2 position = lineStart;
        char previous = '\0';

        void GoToNextLine()
        {
            lineStart.Y += Info.LineHeight * size;
            position = lineStart;
        }

        foreach (var chr in text)
        {
            if (chr == '\n')
            {
                GoToNextLine();
                continue;
            }

            if (!Info.TryGetGlyph(chr, out MsdfGlyph glyph))
            {
                continue;
            }

            if ((position.X + glyph.Advance * size) > maxLineLength)
            {
                GoToNextLine();
            }

            if (Info.TryGetKerning(previous, chr, out float kerningAdvance))
            {
                position.X += kerningAdvance * size;
            }

            if (glyph.PlaneBounds.Size != default && glyph.AtlasBounds.Size != default)
            {
                Vector2 tl = glyph.PlaneBounds.GetTopLeft() * size;
                Vector2 bl = glyph.PlaneBounds.GetBottomLeft() * size;
                Vector2 br = glyph.PlaneBounds.GetBottomRight() * size;
                Vector2 tr = glyph.PlaneBounds.GetTopRight() * size;

                Vector2 ttl = glyph.AtlasBounds.GetTopLeft();
                Vector2 tbl = glyph.AtlasBounds.GetBottomLeft();
                Vector2 tbr = glyph.AtlasBounds.GetBottomRight();
                Vector2 ttr = glyph.AtlasBounds.GetTopRight();

                vertices.Add(new(position + tl, ttl / Info.Size));
                vertices.Add(new(position + bl, tbl / Info.Size));
                vertices.Add(new(position + br, tbr / Info.Size));
                vertices.Add(new(position + tl, ttl / Info.Size));
                vertices.Add(new(position + br, tbr / Info.Size));
                vertices.Add(new(position + tr, ttr / Info.Size));
            }

            position += new Vector2(glyph.Advance * size, 0);
            previous = chr;
        }

        return vertices.ToArray();
    }

    public Box2 MeasureString(string text, float size, float maxLineLength = float.PositiveInfinity)
    {
        Vertex[] vertices = CreateMesh(text, size, maxLineLength);

        if (vertices.Length == 0)
        {
            return default;
        }

        Vector2 min = new(vertices.Min(v => v.Position.X), vertices.Min(v => v.Position.Y));
        Vector2 max = new(vertices.Max(v => v.Position.X), vertices.Max(v => v.Position.Y));

        return new(min, max);
    }

    public MsdfText CreateText(string text, float size, float maxLineLength = float.PositiveInfinity)
    {
        return new MsdfText(this, text, size, maxLineLength);
    }
}
