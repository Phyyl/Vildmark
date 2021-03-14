using OpenTK.Mathematics;
using System.Collections.Generic;
using System.Linq;
using Vildmark.Graphics.Rendering;
using Vildmark.Graphics.Resources;

namespace Vildmark.Graphics.Fonts
{
    //TODO: Switch to "shape" model. Add "PreparedText" and render that instead
    public static class RenderContextFontExtensions
    {
        private const float defaultSize = 24;
        private static Mesh<Vertex> stringMesh;

        public static void RenderString(this RenderContext2D renderContext, string str, Vector2 position = default, float size = defaultSize, Vector4? color = default, Font font = default)
        {
            if (str == null || str.Length == 0)
            {
                return;
            }

            font ??= Font.Arial;
            color ??= Vector4.One;

            renderContext.DisableDepthTest();

            Vertex[] vertices = CreateStringVertices(str, font, position, size);

            if (stringMesh is null)
            {
                stringMesh = new Mesh<Vertex>(vertices);
            }
            else
            {
                stringMesh.VertexBuffer.SetData(vertices);
            }

            stringMesh.Transform.Position = new Vector3(position);

            renderContext.Render(stringMesh, new TextureMaterial(font.Texture, color.Value));
        }

        public static Vector2 MeasureStringLine(this RenderContext2D renderContext, string str, float size = defaultSize, Font font = default)
        {
            if (str == null || str.Length == 0)
            {
                return new Vector2(0, size);
            }

            font ??= Font.Arial;

            Vertex[] vertices = CreateStringVertices(str, font, Vector2.Zero, size);

            float width = vertices.Max(v => v.Position.X);
            float height = vertices.Max(v => v.Position.Y);

            return new Vector2(width, height);
        }

        public static Vector2 MeasureStringBounds(this RenderContext2D renderContext, string str, float size = defaultSize, Font font = default)
        {
            if (str == null || str.Length == 0)
            {
                return new Vector2(0, size);
            }

            font ??= Font.Arial;

            Vertex[] vertices = CreateStringVertices(str, font, Vector2.Zero, size);

            float width = vertices.Max(v => v.Position.X);
            float height = vertices.Max(v => v.Position.Y);

            float x = vertices.Min(v => v.Position.X);
            float y = vertices.Min(v => v.Position.Y);

            return new Vector2(width - x, height - y);
        }

        private static Vertex[] CreateStringVertices(string str, Font font, Vector2 position, float size)
        {
            List<Vertex> vertices = new();

            Vector2 cursor = new(0, size);

            foreach (var chr in str)
            {
                if (!font.Characters.TryGetValue(chr, out FontChar fontChar))
                {
                    continue;
                }

                Vector2 glyphSize = new Vector2(fontChar.Width, fontChar.Height) / font.Size * size;
                Vector2 glyphOrigin = new Vector2(-fontChar.OriginX, -fontChar.OriginY) / font.Size * size;

                Vector3 vtl = new(glyphOrigin + cursor + position);
                Vector3 vtr = vtl + new Vector3(glyphSize.X, 0, 0);
                Vector3 vbl = vtl + new Vector3(0, glyphSize.Y, 0);
                Vector3 vbr = vtl + new Vector3(glyphSize.X, glyphSize.Y, 0);

                Vector2 ts = new(fontChar.Width / (float)font.Width, fontChar.Height / (float)font.Height);
                Vector2 ttl = new(fontChar.X / (float)font.Width, fontChar.Y / (float)font.Height);
                Vector2 ttr = ttl + new Vector2(ts.X, 0);
                Vector2 tbl = ttl + new Vector2(0, ts.Y);
                Vector2 tbr = ttl + ts;

                vertices.Add(new Vertex(vtl, ttl));
                vertices.Add(new Vertex(vbl, tbl));
                vertices.Add(new Vertex(vbr, tbr));
                vertices.Add(new Vertex(vtl, ttl));
                vertices.Add(new Vertex(vbr, tbr));
                vertices.Add(new Vertex(vtr, ttr));

                cursor.X += fontChar.Advance / (float)font.Size * size;
            }

            return vertices.ToArray();
        }
    }
}
