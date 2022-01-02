using OpenTK.Mathematics;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using Vildmark.Graphics.Fonts.Resources;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Rendering;
using Vildmark.Resources;

namespace Vildmark.Graphics.Fonts
{
    public record BitmapFontChar(char Character, int X, int Y, int Width, int Height, int XOffset, int YOffset, int XAdvance, int Page);

    public class BitmapFont
    {
        private readonly BitmapTextModel model;
        private readonly Dictionary<char, BitmapFontChar> characters;

        public Texture2D[] Pages { get; internal init; }
        public string Name { get; internal init; }
        public int Size { get; internal init; }
        public int LineHeight { get; internal init; }
        public int Base { get; internal init; }

        internal BitmapFont(BitmapFontChar[] chars)
        {
            characters = chars.ToDictionary(c => c.Character);

            model = new();
        }

        public bool TryGetChar(char character, out BitmapFontChar bitmapChar)
        {
            return characters.TryGetValue(character, out bitmapChar);
        }

        public bool TryGetChar(char character, char fallback, out BitmapFontChar bitmapChar)
        {
            return TryGetChar(character, out bitmapChar) || TryGetChar(fallback, out bitmapChar);
        }

        public BitmapTextModel CreateModel(string text, float size, Color4 color)
        {
            BitmapTextModel model = new();

            UpdateModel(model, text, size, color);

            return model;
        }

        public void UpdateModel(BitmapTextModel model, string text, float size, Color4 color)
        {
            model.Mesh.UpdateVertices(CreateStringVertices(text, size));
            model.Material = new TextMaterial()
            {
                Textures = Pages,
                Tint = color
            };
        }

        public RectangleF GetBounds(string text, float size, Vector2 position = default)
        {
            BitmapTextVertex[] vertices = CreateStringVertices(text, size);

            float minX = vertices.Min(v => v.Position.X);
            float minY = vertices.Min(v => v.Position.Y);
            float maxX = vertices.Max(v => v.Position.X);
            float maxY = vertices.Max(v => v.Position.Y);

            return new RectangleF(minX + position.X, minY + position.Y, maxX - minX, maxY - minY);
        }

        public void RenderString(RenderContext renderContext, string text, Vector2 position, float size, Color4 color)
        {
            UpdateModel(model, text, size, color);
            model.Transform.Position = new Vector3(position);

            renderContext.Blending = true;
            renderContext.DepthTest = false;
            model.Render(renderContext);
        }

        private BitmapTextVertex[] CreateStringVertices(string text, float size)
        {
            if (text is null)
            {
                return Array.Empty<BitmapTextVertex>();
            }

            // Convert to 0..1
            size /= Size;

            List<BitmapTextVertex> vertices = new();

            Vector2 cursor = new Vector2(0, Base / (float)LineHeight * size);

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

                vertices.Add(new BitmapTextVertex(vtl, ttl, fontChar.Page));
                vertices.Add(new BitmapTextVertex(vbl, tbl, fontChar.Page));
                vertices.Add(new BitmapTextVertex(vbr, tbr, fontChar.Page));
                vertices.Add(new BitmapTextVertex(vtl, ttl, fontChar.Page));
                vertices.Add(new BitmapTextVertex(vbr, tbr, fontChar.Page));
                vertices.Add(new BitmapTextVertex(vtr, ttr, fontChar.Page));

                cursor.X += fontChar.XAdvance * size;
            }

            return vertices.ToArray();
        }
    }
}
