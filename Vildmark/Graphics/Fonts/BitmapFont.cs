using OpenTK.Mathematics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using Vildmark.Graphics.Fonts.Loaders;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Meshes;
using Vildmark.Graphics.Rendering;
using Vildmark.Graphics.Textures;
using Vildmark.Resources;

namespace Vildmark.Graphics.Fonts;

public record BitmapFontChar(char Character, int X, int Y, int Width, int Height, int XOffset, int YOffset, int XAdvance, int Page);

public class BitmapFont : IResource<BitmapFont>
{
    public static IResourceLoader<BitmapFont> Loader { get; } = new BitmapFontLoader();
    public static readonly BitmapFontShader bitmapFontShader = new();

    private readonly Mesh<BitmapFontVertex> mesh;
    private readonly Dictionary<char, BitmapFontChar> characters;

    public GLTexture2D[] Pages { get; internal init; }
    public string Name { get; internal init; }
    public int Size { get; internal init; }
    public int LineHeight { get; internal init; }
    public int Base { get; internal init; }

    internal BitmapFont(BitmapFontChar[] chars)
    {
        characters = chars.ToDictionary(c => c.Character);

        mesh = new Mesh<BitmapFontVertex>();
        Pages = Array.Empty<GLTexture2D>();
        Name = "";
    }
    
    public bool TryGetChar(char character, [NotNullWhen(true)] out BitmapFontChar? bitmapChar)
    {
        return characters.TryGetValue(character, out bitmapChar);
    }

    public bool TryGetChar(char character, char fallback, [NotNullWhen(true)] out BitmapFontChar? bitmapChar)
    {
        return TryGetChar(character, out bitmapChar) || TryGetChar(fallback, out bitmapChar);
    }

    public RectangleF GetBounds(string text, float size, Vector2 position = default)
    {
        BitmapFontVertex[] vertices = CreateStringVertices(text, size);

        float minX = vertices.Min(v => v.Position.X);
        float minY = vertices.Min(v => v.Position.Y);
        float maxX = vertices.Max(v => v.Position.X);
        float maxY = vertices.Max(v => v.Position.Y);

        return new RectangleF(minX + position.X, minY + position.Y, maxX - minX, maxY - minY);
    }

    public void RenderString(Renderer renderContext, string text, Vector2 position, float size, Color4 color)
    {
        UpdateMesh(mesh, text, size);

        renderContext.Blending = true;
        renderContext.Render(mesh, new BitmapFontMaterial(Pages, color), bitmapFontShader, new Transform() { Position = new Vector3(position) });
    }

    public void UpdateMesh(Mesh<BitmapFontVertex> mesh, string text, float size)
    {
        mesh.UpdateVertices(CreateStringVertices(text, size));
    }

    private BitmapFontVertex[] CreateStringVertices(string text, float size)
    {
        if (text is null || size <= 0)
        {
            return Array.Empty<BitmapFontVertex>();
        }

        // Convert to 0..1
        size /= Size;

        List<BitmapFontVertex> vertices = new();

        Vector2 cursor = new(0, Base / (float)LineHeight * size);

        foreach (var chr in text)
        {
            if (!TryGetChar(chr, ' ', out BitmapFontChar? fontChar))
            {
                continue;
            }

            Texture2D page = Pages[fontChar.Page];

            Vector2 sourcePosition = new(fontChar.X / (float)page.Width, fontChar.Y / (float)page.Height);
            Vector2 sourceSize = new(fontChar.Width / (float)page.Width, fontChar.Height / (float)page.Height);
            Vector2 destinationPosition = new Vector2(fontChar.XOffset, fontChar.YOffset) * size;
            Vector2 destinationSize = new Vector2(fontChar.Width, fontChar.Height) * size;

            Vector2 vtl = cursor + destinationPosition;
            Vector2 vtr = vtl + new Vector2(destinationSize.X, 0);
            Vector2 vbl = vtl + new Vector2(0, destinationSize.Y);
            Vector2 vbr = vtl + destinationSize;

            Vector2 ttl = sourcePosition;
            Vector2 ttr = ttl + new Vector2(sourceSize.X, 0);
            Vector2 tbl = ttl + new Vector2(0, sourceSize.Y);
            Vector2 tbr = ttl + sourceSize;

            vertices.Add(new BitmapFontVertex(vtl, ttl, fontChar.Page));
            vertices.Add(new BitmapFontVertex(vbl, tbl, fontChar.Page));
            vertices.Add(new BitmapFontVertex(vbr, tbr, fontChar.Page));
            vertices.Add(new BitmapFontVertex(vtl, ttl, fontChar.Page));
            vertices.Add(new BitmapFontVertex(vbr, tbr, fontChar.Page));
            vertices.Add(new BitmapFontVertex(vtr, ttr, fontChar.Page));

            cursor.X += fontChar.XAdvance * size;
        }

        return vertices.ToArray();
    }
}
