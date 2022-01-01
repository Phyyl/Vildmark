using OpenTK.Mathematics;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Rendering;
using Vildmark.Resources;

namespace Vildmark.Graphics.Fonts
{
    public record BitmapFontChar(char Character, int X, int Y, int Width, int Height, int XOffset, int YOffset, int XAdvance, int Page);

    public class BitmapFont
    {
        private readonly TextShader shader;

        private readonly Dictionary<char, BitmapFontChar> characters;

        public Texture2D[] Pages { get; internal init; }

        public string Name { get; internal init; }

        public int Size { get; internal init; }

        public int LineHeight { get; internal init; }

        public int Base { get; internal init; }

        internal BitmapFont(BitmapFontChar[] chars)
        {
            characters = chars.ToDictionary(c => c.Character);

            shader = new TextShader();
        }

        public bool TryGetChar(char character, out BitmapFontChar bitmapChar)
        {
            return characters.TryGetValue(character, out bitmapChar);
        }

        public bool TryGetChar(char character, char fallback, out BitmapFontChar bitmapChar)
        {
            return TryGetChar(character, out bitmapChar) || TryGetChar(fallback, out bitmapChar);
        }

        public TextModel CreateModel(string text, float size, Color4 color)
        {
            return new TextModel(new(CreateStringVertices(text, size)), new TextMaterial { Textures = Pages, Tint = color });
        }

        public void UpdateModel(TextModel model, string text, float size, Color4 color)
        {
            model.Mesh.UpdateVertices(CreateStringVertices(text, size));
            model.Material = new TextMaterial()
            {
                Textures = Pages,
                Tint = color
            };
        }

        private TextVertex[] CreateStringVertices(string text, float size)
        {
            text ??= "";

            // Convert to 0..1
            size /= Size;

            List<TextVertex> vertices = new();

            Vector2 cursor = new Vector2(0, Base);

            foreach (var chr in text)
            {
                if (!TryGetChar(chr, ' ', out BitmapFontChar fontChar))
                {
                    continue;
                }

                Texture2D page = Pages[fontChar.Page];

                Vector2 sourcePosition = new Vector2(fontChar.X / (float)page.Width, fontChar.Y / (float)page.Height);
                Vector2 sourceSize = new Vector2(fontChar.Width / (float)page.Width, fontChar.Height / (float)page.Height);
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

                vertices.Add(new TextVertex(vtl, ttl, fontChar.Page));
                vertices.Add(new TextVertex(vbl, tbl, fontChar.Page));
                vertices.Add(new TextVertex(vbr, tbr, fontChar.Page));
                vertices.Add(new TextVertex(vtl, ttl, fontChar.Page));
                vertices.Add(new TextVertex(vbr, tbr, fontChar.Page));
                vertices.Add(new TextVertex(vtr, ttr, fontChar.Page));

                cursor.X += fontChar.XAdvance * size;
            }

            return vertices.ToArray();
        }
    }
}
