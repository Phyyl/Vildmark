using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.Models;
using Vildmark.Graphics.Rendering;

namespace Vildmark.Graphics.Fonts
{
    public static class RenderContextFontExtensions
    {
        private const float defaultSize = 24;
        private static Mesh stringMesh;

        public static void RenderString(this RenderContext2D renderContext, string str, Vector2 position = default, float size = defaultSize, Vector4? color = default, Font font = default, Vector2 origin = default)
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
                stringMesh = new Mesh(vertices);
            }
            else
            {
                stringMesh.UpdateVertices(vertices);
            }

            Matrix4 modelMatrix = Matrix4.CreateTranslation(new Vector3(-origin));

            renderContext.Render(stringMesh, new Material(font.Texture, color.Value), modelMatrix: modelMatrix,shader: Resources.Shaders.DistanceField);
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
            List<Vertex> vertices = new List<Vertex>();

            Vector2 cursor = new Vector2(0, size);

            foreach (var chr in str)
            {
                if (!font.Characters.TryGetValue(chr, out FontChar fontChar))
                {
                    continue;
                }

                Vector2 glyphSize = new Vector2(fontChar.Width, fontChar.Height) / font.Size * size;
                Vector2 glyphOrigin = new Vector2(-fontChar.OriginX, -fontChar.OriginY) / font.Size * size;

                Vector3 vtl = new Vector3(glyphOrigin + cursor + position);
                Vector3 vtr = vtl + new Vector3(glyphSize.X, 0, 0);
                Vector3 vbl = vtl + new Vector3(0, glyphSize.Y, 0);
                Vector3 vbr = vtl + new Vector3(glyphSize.X, glyphSize.Y, 0);

                Vector2 ts = new Vector2(fontChar.Width / (float)font.Width, fontChar.Height / (float)font.Height);
                Vector2 ttl = new Vector2(fontChar.X / (float)font.Width, fontChar.Y / (float)font.Height);
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
